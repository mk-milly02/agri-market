using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgriMarket.API.Models.Domain.Auth;
using AgriMarket.API.Repositories.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AgriMarket.API.Repositories.Auth.Implementations
{
    public class TokenRepository : ITokenRepository
    {

        public string CreateJWTToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}