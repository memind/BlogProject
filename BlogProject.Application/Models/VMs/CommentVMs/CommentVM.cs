
namespace BlogProject.Application.Models.VMs.CommentVMs
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string CommentedBy { get; set; } // user
        public string CommentedTo { get; set; } // post
    }
}
