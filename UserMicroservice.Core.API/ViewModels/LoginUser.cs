using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Core.API.ViewModels
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        public string Password { get; set; }
    }
}
