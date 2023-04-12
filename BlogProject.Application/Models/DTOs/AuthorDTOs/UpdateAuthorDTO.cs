
using BlogProject.Application.Extensions;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Application.Models.DTOs.AuthorDTOs
{
    public class UpdateAuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public string? ImagePath { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;

        [Required(ErrorMessage = "Must to type Post")]
        public int PostId { get; set; }
        public List<PostVM>? Posts { get; set; }
    }
}
