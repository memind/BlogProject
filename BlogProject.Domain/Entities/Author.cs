using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Domain.Entities
{
    public class Author : IBaseEntity
    {
        public Author()
        {
            Posts = new List<Post>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }

        // Interface Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Properties
        public List<Post> Posts { get; set; }
    }
}
