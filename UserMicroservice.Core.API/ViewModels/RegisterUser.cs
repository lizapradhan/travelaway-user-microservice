using System.ComponentModel.DataAnnotations;
using UserMicroservice.Core.API.Enums;

namespace UserMicroservice.Core.API.ViewModels
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First name must contain only alphabetical letters without spaces.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last name must contain only alphabetical letters without spaces.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression("^[1-9][0-9]{9}$", ErrorMessage = "Contact number must be a 10-digit number and cannot start with zero.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(RegisterUser), "ValidateDateOfBirth")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [RegularExpression("^[a-zA-Z0-9\\s\\-.,#]*$", ErrorMessage = "Address can contain alphanumeric characters and special characters like '-', ',', or '#'.")]
        public string Address { get; set; }

        // Custom validation method for Date of Birth
        public static ValidationResult ValidateDateOfBirth(DateTime dob, ValidationContext context)
        {
            if (dob >= DateTime.Now)
            {
                return new ValidationResult("Date of Birth must be a valid date and less than the current date.");
            }
            return ValidationResult.Success;
        }
    }
}
