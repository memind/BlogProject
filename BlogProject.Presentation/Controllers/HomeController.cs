using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            List<PostVM> posts = await _postService.GetPosts();
            return View(posts);
        }

        public async Task<IActionResult> Post(int id)
        {
            return View(await _postService.GetPostDetails(id));
        }

       
    }
}