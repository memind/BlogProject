using BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Domain.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public AppUser()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }

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
    }
}
