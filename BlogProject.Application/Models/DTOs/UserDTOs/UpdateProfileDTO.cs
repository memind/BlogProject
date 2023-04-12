using BlogProject.Application.Extensions;
using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace BlogProject.Application.Models.DTOs.UserDTOs
{
    public class UpdateProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Email { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }

    }
}
