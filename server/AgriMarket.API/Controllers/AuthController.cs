using AgriMarket.API.Models.DTO.Auth.Requests;
using AgriMarket.API.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgriMarket.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository auth;

        public AuthController(IAuthRepository auth)
        {
            this.auth = auth;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            var res = await auth.SignIn(signInRequestDTO);

            if (res == null)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            var res = await auth.SignUp(signUpRequestDTO);

            if (res == null)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}