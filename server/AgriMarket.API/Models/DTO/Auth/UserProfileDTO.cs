using System.ComponentModel.DataAnnotations;

namespace AgriMarket.API.Models.DTO.Auth
{
    public class UserProfileDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "This must be a valid email")]
        public required string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First Name must be a minimum of 2 characters")]
        [MaxLength(100, ErrorMessage = "First Name must be a maximum of 100 characters")]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be a minimum of 2 characters")]
        [MaxLength(100, ErrorMessage = "Last Name must be a maximum of 100 characters")]
        public required string LastName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Username must be a minimum of 2 characters")]
        [MaxLength(20, ErrorMessage = "Username must be a maximum of 100 characters")]
        public required string UserName { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}