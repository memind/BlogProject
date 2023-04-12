using BlogProject.Domain.Enums;

namespace BlogProject.Application.Models.DTOs.UserDTOs
{
    public class RegisterDTO
    {
        // ToDo: Data Annotations
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
