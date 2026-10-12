using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Data.Helpers.Enums;
using SocialApp.Data.Models;
using SocialApp.Data.Services;
using SocialApp.ViewModels.Stories;

namespace SocialApp.Controllers
{
    public class StoriesController : Controller
    {

        private readonly IStoriesService _storiesService;
        private readonly IFilesService _filesService;

        public StoriesController(IStoriesService storiesService, IFilesService filesService)
        {
            _storiesService = storiesService;
            _filesService = filesService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateStory(StoryVM storyVM)
        {
            // Get the logged in user
            int loggedInUser = 1;

            var imageUploadPath = await _filesService.UploadImageAsync(storyVM.Image, ImageFileType.StoryImage);

            // Create a new story
            var newStory = new Story
            {
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
                ImageUrl = imageUploadPath,
                UserId = loggedInUser,
            };

            await _storiesService.CreateStoryAsync(newStory);

            // Redirect to the index page
            return RedirectToAction("Index", "Home");
        }
    }
}
