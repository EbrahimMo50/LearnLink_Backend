using LearnLink_Backend.DTOs;

namespace LearnLink_Backend.Services.JWTService
{
    public interface ITokenService
    {
        public string GenerateToken(UniversalUser user);
    }
}
