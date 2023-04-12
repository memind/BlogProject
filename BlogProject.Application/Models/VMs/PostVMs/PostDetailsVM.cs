using BlogProject.Application.Models.VMs.CommentVMs;

namespace BlogProject.Application.Models.VMs.PostVMs
{
    public class PostDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string FullName => $"{AuthorFirstName} {AuthorLastName}";
        public string AuthorImagePath { get; set; }

        public List<CommentVM> Comments { get; set; }
    }
}
