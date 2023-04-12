using AutoMapper;
using BlogProject.Application.Models.DTOs.UserDTOs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Application.Services.AppUserService
{
    internal class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, global::AutoMapper.IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    ImagePath = x.ImagePath
                },
                where: x => x.UserName == userName
                );

            return result;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            AppUser user = new AppUser();

            user.UserName = model.Username;
            user.Email = model.Email;
            user.CreateDate = model.CreateDate;

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                await _signInManager.SignInAsync(user, isPersistent: false);

            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            // Update islemlerinde once id ile ilgili nesne RAM'e cekilir. Disaridan gelen guncel bilgilerle degisiklikler yapilir. En son SaveChange() ile guncellemeler veritabanina gonderilir.

            var user2 = _mapper.Map<AppUser>(model);

            AppUser user = await _appUserRepository.GetDefault(x => x.Id == user2.Id);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(300, 300));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                user.ImagePath = $"/images/{guid}.jpg";
            }

            else
            {
                if (model.ImagePath != null)
                    user.ImagePath = model.ImagePath;

                else
                    user.ImagePath = $"/images/defaultuser.jpg";
            }

            //user.Status = Domain.Enums.Status.Modified;
            //user.UpdateDate = DateTime.Now;
            //user.UserName = model.UserName;

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

                await _userManager.UpdateAsync(user);
            }

            if (model.Email != null)
            {
                AppUser isUserEmailExist = await _userManager.FindByEmailAsync(model.Email);

                if (isUserEmailExist == null)
                    await _userManager.SetEmailAsync(user, model.Email);
            }

            await _appUserRepository.Update(user);
        }
    }
}
