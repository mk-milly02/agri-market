using System.ComponentModel.DataAnnotations;

namespace AgriMarket.API.Models.DTO.Auth.Responses
{
    public class SignInResponseDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "This must be a valid email")]
        public required string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Username must be a minimum of 2 characters")]
        [MaxLength(20, ErrorMessage = "Username must be a maximum of 100 characters")]
        public required string UserName { get; set; }
        public string JWTToken { get; set; }
    }
}