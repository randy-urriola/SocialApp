using System.Diagnostics;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Data.Helpers;
using SocialApp.Data.Models;
using SocialApp.Data.Services;
using SocialApp.ViewModels.Home;

namespace SocialApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IPostsService _postsService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IPostsService postsService)
        {
            _logger = logger;
            _context = context;
            _postsService = postsService;
        }

        public async Task<IActionResult> Index()
        {

            int loggedInUserId = 1;
            var AllPosts = await _postsService.GetAllPostsAsync(loggedInUserId);

            return View(AllPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
        {
            // Get the logged in user
            int loggedInUser = 1;

            // Create a new post 
            var newPost = new Post
            {
                Content = post.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                ImageUrl = "",
                UserId = loggedInUser,
            };

            await _postsService.CreatePostAsync(newPost, post.Image);


            // find and store hash tags
            //var postHashTags = HashtagHelper.GetHashtags(post.Content);
            //foreach (var hashTag in postHashTags)
            //{
            //    var hashTagDb = await _context.Hashtags.FirstOrDefaultAsync(h => h.Name == hashTag);
            //    if (hashTagDb != null)
            //    {
            //        hashTagDb.Count += 1;
            //        hashTagDb.DateUpdated = DateTime.UtcNow;

            //        _context.Hashtags.Update(hashTagDb);
            //        await _context.SaveChangesAsync();
            //    }
            //    else
            //    {
            //        var newHashTag = new Hashtag()
            //        {
            //            Name = hashTag,
            //            Count = 1,
            //            DateCreated = DateTime.UtcNow,
            //            DateUpdated = DateTime.UtcNow
            //        };

            //        await _context.Hashtags.AddAsync(newHashTag);
            //        await _context.SaveChangesAsync();
            //    }
            //}

            // Redirect to the index page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLikeVM postLikeVM)
        {
            int loggedInUserId = 1;
            await _postsService.TogglePostLikeAsync(postLikeVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostFavorite(PostFavoriteVM postFavoriteVM)
        {
            int loggedInUserId = 1;
            await _postsService.TogglePostFavoriteAsync(postFavoriteVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TogglePostVisibility(PostVisibilityVM postVisibilityVM)
        {
            int loggedInUserId = 1;
            await _postsService.TogglePostVisibilityAsync(postVisibilityVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostCommentVM postCommentVM)
        {
            int loggedInUserId = 1;

            // Create a post object
            var newComment = new Comment()
            {
                UserId = loggedInUserId,
                PostId = postCommentVM.PostId,
                Content = postCommentVM.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };


            await _postsService.AddPostCommentAsync(newComment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddPostReport(PostReportVM postReportVM)
        {
            int loggedInUserId = 1;
            await _postsService.ReportPostAsync(postReportVM.PostId, loggedInUserId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemoveCommentVM removeCommentVM)
        {
            await _postsService.RemovePostCommentAsync(removeCommentVM.CommentId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PostRemove(PostRemoveVM postRemoveVM)
        {
            await _postsService.RemovePostAsync(postRemoveVM.PostId);



            // Update hashtags count for the removed post
            //var postHashTags = HashtagHelper.GetHashtags(postDb.Content);
            //foreach (var hashtag in postHashTags)
            //{
            //    var hashtagDb = await _context.Hashtags.FirstOrDefaultAsync(n => n.Name == hashtag);
            //    if (hashtagDb != null)
            //    {
            //        hashtagDb.Count -= 1;
            //        hashtagDb.DateUpdated = DateTime.UtcNow;

            //        _context.Hashtags.Update(hashtagDb);
            //        await _context.SaveChangesAsync();
            //    }
            //}




            return RedirectToAction("Index");
        }

    }
}
