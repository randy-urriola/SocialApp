using Microsoft.AspNetCore.Mvc;
using SocialApp.Data.Services;

namespace SocialApp.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IPostsService _postsService;
        public FavoritesController(IPostsService postsService)
        {
            _postsService = postsService;
        }
        public async Task<IActionResult> Index()
        {
            int loggedInUserId = 1; // Replace with the actual logged-in user ID
            var myFavoritedPosts = await _postsService.GetAllFavoritedPostsAsync(loggedInUserId);

            return View(myFavoritedPosts);
        }
    }
}
