using LearnLink_Backend.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace LearnLink_Backend.Modules.Authentcation.Services.Token
{
    public interface ITokenService
    {
        public string GenerateToken(UniversalUser user);
    }
}
