namespace SocialApp.ViewModels.Home
{
    public class PostVM
    {
        public string Content { get; set; }
        public IFormFile Image { get; set; } // represent a file witch can be sent over a http request
    }
}
