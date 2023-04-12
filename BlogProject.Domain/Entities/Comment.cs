using BlogProject.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Domain.Entities
{
    public class Comment : IBaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Comment must be filled!")]
        [MaxLength(250)]
        public string Content { get; set; }

        // Interface Properties
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Navigation Properties
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
