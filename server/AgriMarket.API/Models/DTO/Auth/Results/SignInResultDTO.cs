using AgriMarket.API.Models.DTO.Auth.Responses;

namespace AgriMarket.API.Models.DTO.Auth.Results
{
    public class SignInResultDTO
    {
        public SignInResponseDTO? Data { get; set; }
        public string? Error { get; set; }
        public bool Success => Error == null; // Success is a boolean whose value is based on whether "Error" is null or not.
    }
}