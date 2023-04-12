using BlogProject.Application.Extensions;
using BlogProject.Application.Models.VMs.AuthorVMs;
using BlogProject.Application.Models.VMs.GenreVMs;
using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Application.Models.DTOs.PostDTOs
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must to type Title")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must to type Content")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Content { get; set; }
        public string ImagePath { get; set; }

        // Custom Extension yazacağız.
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Modified; [Required(ErrorMessage = "Must to type Author")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Must to type Genre")]
        public int GenreId { get; set; }              
        
        // Genre ve Author CM listerleri doldurulacak 
        public List<GenreVM>? Genres { get; set; }
        public List<AuthorVM>? Authors { get; set; }
    }
}
