using BlogProject.Application.Extensions;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Application.Models.DTOs.AuthorDTOs
{
    public class CreateAuthorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public int PostId { get; set; }
        public List<PostVM>? Posts => null;
    }
}
