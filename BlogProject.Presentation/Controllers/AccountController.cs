using BlogProject.Application.Models.DTOs.UserDTOs;
using BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    [Authorize] // Buradaki actionlara yetkisiz kisilerin istekte bulunmasi engellenir.
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) // kullanici giris yaptiysa
            {
                return RedirectToAction("Index", "");
            }
            return View(); // kullanici giris yapmadiysa
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Something went wrong.";
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View();
        }

        public IActionResult RedirectToLocal(string returnUrl = "/")
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            else
                return RedirectToAction("Index", "");

        }

        public async Task<IActionResult> Logout()
        {
            await _appUserService.LogOut();
            return RedirectToAction("Index", "Home");
        }

        // Profile Details
        public async Task<IActionResult> ProfileDetails(string userName)
        {
            return View(await _appUserService.GetByUserName(userName));
        }

        public async Task<IActionResult> Edit(string username)
        {
            if (username != "")
            {
                UpdateProfileDTO user = await _appUserService.GetByUserName(username);
                return View(user);
            }

            else if (username == "")
            {
                username = HttpContext.User.Identity.Name;
                UpdateProfileDTO user = await _appUserService.GetByUserName(username);
                return View(user);
            }

            else
                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            if (model.ImagePath == null)
            {
                model.ImagePath = "~/wwwroot/images/defaultuser.jpg";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _appUserService.UpdateUser(model);

                }
                catch (Exception)
                {
                    TempData["Error"] = "Something went wrong.";
                }

                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated!";
                return View();
            }
        }
    }
}
