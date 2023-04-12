using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Application.Extensions
{
    public class PictureFileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value; // value as IFormFile
            
            if (file != null )
            {
                var extension = Path.GetExtension(file.FileName).ToLower(); // dosyanin uzanti kismini (extension) kismini alip ToLower() ediyor.

                string[] extensions = { ".jpeg", ".jpg", ".png" };
                bool result = extensions.Any(x => x.EndsWith(extension));

                if (!result)
                {
                    return new ValidationResult("Valid format is 'jpeg', 'jpg' 'png'");
                }
            }

            return ValidationResult.Success;
        }
    }
}
