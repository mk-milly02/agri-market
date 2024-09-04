using AgriMarket.API.Models.Domain.Auth;
using AgriMarket.API.Models.DTO.Auth.Requests;
using AgriMarket.API.Models.DTO.Auth.Responses;
using AgriMarket.API.Models.DTO.Auth.Results;
using AgriMarket.API.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AgriMarket.API.Repositories.Auth.Implementations
{
    public class AuthRepository(UserManager<User> userManager, ITokenRepository tokenRepository, IEmailSender<User> emailSender) : IAuthRepository
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly ITokenRepository tokenRepository = tokenRepository;
        private readonly IEmailSender<User> emailSender = emailSender;

        public async Task<SignInResultDTO> SignIn(SignInRequestDTO signInRequest)
        {
            var result = new SignInResultDTO();

            // Get the user from the auth DB
            var user = await userManager.FindByEmailAsync(signInRequest.Email);

            if (user == null)
            {
                result.Error = "User not found";
                return result;
            }

            var passwordIsValid = await userManager.CheckPasswordAsync(user, signInRequest.Password);
            if (!passwordIsValid)
            {
                result.Error = "Invalid password";
                return result;
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
            {
                result.Error = "User has no roles assigned.";
                return result;
            }

            var jwt = tokenRepository.CreateJWTToken(user, roles.ToList());

            result.Data = new SignInResponseDTO
            {
                UserId = Guid.Parse(user.Id),
                Email = user.Email,
                UserName = user.UserName,
                JWTToken = jwt
            };

            return result;
        }

        public async Task<IdentityResult> SignUp(SignUpRequestDTO signUpRequest)
        {
            // Create a new IdentityUser
            var user = new User
            {
                PhoneNumber = signUpRequest.PhoneNumber,
                FirstName = signUpRequest.FirstName,
                LastName = signUpRequest.LastName,
                UserName = signUpRequest.UserName,
                Email = signUpRequest.Email,
            };

            // Create the user in the auth db
            var res = await userManager.CreateAsync(user, signUpRequest.Password);

            // If user creation was successful, add the necessary role
            if (res.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await emailSender.SendConfirmationLinkAsync(user, signUpRequest.Email, token);
                await userManager.AddToRolesAsync(user, signUpRequest.Roles);
            }
            return res;
        }

    }
}