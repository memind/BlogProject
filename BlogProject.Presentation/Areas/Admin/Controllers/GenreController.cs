using BlogProject.Application.Models.DTOs.GenreDTOs;
using BlogProject.Application.Services.GenreService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // Genre list
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetGenres());
        }

        // Genre create
        public IActionResult Create()
        {
            return View();
        }

        // Genre create post
        [HttpPost]
        public async Task<IActionResult> Create(GenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Create(genre);
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // Genre update
        public async Task<IActionResult> Update(int id)
        {
            return View(await _genreService.GetById(id));
        }

        // Genre update post
        [HttpPost]
        public async Task<IActionResult> Update(GenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Update(genre);
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // Delete genre
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
