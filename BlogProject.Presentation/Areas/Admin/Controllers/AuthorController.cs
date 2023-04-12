using BlogProject.Application.Models.DTOs.AuthorDTOs;
using BlogProject.Application.Models.VMs.AuthorVMs;
using BlogProject.Application.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            List<AuthorVM> authors = await _authorService.GetAuthors();
            return View(authors);
        }

        // create author
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // create author post
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDTO author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Create(author);
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // update author
        public async Task<IActionResult> Update(UpdateAuthorDTO author)
        {
            return View(author);
        }

        // update author post
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorDTO author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Update(author);
                return RedirectToAction("Index");
            }
            return View(author);  
        }

        // delete
        public async Task<IActionResult> Delete(UpdateAuthorDTO author)
        {
            await _authorService.Delete(author.Id);
            return RedirectToAction("Index");
        }

        // Details
        public async Task<IActionResult> Details(int id)
        {
            return View(await _authorService.GetAuthorDetails(id));
        }
    }
}
