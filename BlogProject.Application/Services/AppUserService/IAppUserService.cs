using BlogProject.Application.Models.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task<UpdateProfileDTO> GetByUserName(string userName);
        Task UpdateUser(UpdateProfileDTO model);

        Task LogOut();
    }
}
