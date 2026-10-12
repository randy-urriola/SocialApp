using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Data.Services;

namespace SocialApp.ViewComponents
{
    public class StoriesViewComponent:ViewComponent
    {
        private readonly IStoriesService _storiesService;

        public StoriesViewComponent(IStoriesService storiesService)
        {
            _storiesService = storiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allStories = await _storiesService.GetAllStoriesAsync();
            return View(allStories);
        }
    }
}
