using BlogProject.Application.Models.DTOs.PostDTOs;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Application.Services.AuthorService;
using BlogProject.Application.Services.GenreService;
using BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        static CreatePostDTO postDto;
        public PostController(IPostService postService, IGenreService genreService, IAuthorService authorService)
        {
            _postService = postService;
            _genreService = genreService;
            _authorService = authorService;
        }

        // List post
        public async Task<IActionResult> Index()
        {
            postDto = await _postService.CreatePost();
            List<PostVM> posts = await _postService.GetPosts();
            return View(posts);
        }

        // create post
        public async Task<IActionResult> Create()
        {
            CreatePostDTO createPostDTO = await _postService.CreatePost();
            ViewBag.Genres = new SelectList(postDto.Genres, "Id", "Name");
            ViewBag.Authors = new SelectList(postDto.Authors, "Id", "FullName");
            return View();
        }

        // create post post
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO post)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(post);
                return RedirectToAction("Index");
            }


            ViewBag.Genres = new SelectList(postDto.Genres, "Id", "Name");
            ViewBag.Authors = new SelectList(postDto.Authors, "Id", "FullName");

            return View(post);
        }

        // update post
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Genres = new SelectList(postDto.Genres, "Id", "Name");
            ViewBag.Authors = new SelectList(postDto.Authors, "Id", "FullName");

            return View(await _postService.GetById(id));
        }

        // update post post
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDTO post)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(post);
                return RedirectToAction("Index");
            }

            ViewBag.Genres = new SelectList(postDto.Genres, "Id", "Name");
            ViewBag.Authors = new SelectList(postDto.Authors, "Id", "FullName");

            return View(post);
        }

        // delete post get
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _postService.GetById(id));
        }

        // delete confirm post
        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePostDTO post)
        {
            await _postService.Delete(post.Id);
            return RedirectToAction("Index");
        }

        // details

        public async Task<IActionResult> Details(int id)
        {
            return View(await _postService.GetPostDetails(id));
        }
    }
}
