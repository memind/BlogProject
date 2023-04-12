namespace BlogProject.Application.Models.VMs.PostVMs
{
    public class PostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string GenreName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string ImagePath { get; set; }
        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}"; 
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
