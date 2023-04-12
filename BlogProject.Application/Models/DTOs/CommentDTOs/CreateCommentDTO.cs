
using BlogProject.Domain.Entities;

namespace BlogProject.Application.Models.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string Content { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
