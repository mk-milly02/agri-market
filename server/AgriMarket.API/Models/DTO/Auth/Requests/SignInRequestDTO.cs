using System.ComponentModel.DataAnnotations;

namespace AgriMarket.API.Models.DTO.Auth.Requests
{
    public class SignInRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "This must be a valid email")]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(2, ErrorMessage = "Password must be a minimum of 2 characters")]
        public required string Password { get; set; }
    }
}