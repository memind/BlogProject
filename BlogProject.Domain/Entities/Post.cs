using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Domain.Entities
{
    public class Post : IBaseEntity
    {
        public Post()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }

        // Interface Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Properties
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
