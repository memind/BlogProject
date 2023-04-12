using BlogProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Domain.Entities
{
    public class Genre : IBaseEntity
    {
        public Genre()
        {
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Name must be filled!")]
        public string Name { get; set; }

        // Interface Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Properties
        public List<Post> Posts { get; set; }
    }
}
