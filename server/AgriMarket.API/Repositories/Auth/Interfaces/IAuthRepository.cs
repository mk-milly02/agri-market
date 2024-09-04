using AgriMarket.API.Models.DTO.Auth.Requests;
using AgriMarket.API.Models.DTO.Auth.Results;
using Microsoft.AspNetCore.Identity;

namespace AgriMarket.API.Repositories.Auth.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> SignUp(SignUpRequestDTO signUpRequest);
        Task<SignInResultDTO> SignIn(SignInRequestDTO signInRequest);
    }
}