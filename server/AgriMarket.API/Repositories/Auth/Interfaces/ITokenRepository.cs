using AgriMarket.API.Models.Domain.Auth;
using Microsoft.AspNetCore.Identity;

namespace AgriMarket.API.Repositories.Auth.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}