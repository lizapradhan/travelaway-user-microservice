using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UserMicroservice.Core.API.Enums;

namespace UserMicroservice.Core.API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } // Primary Key, Auto-increment

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{9}$")]
        public string ContactNumber { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default value
    }
}
