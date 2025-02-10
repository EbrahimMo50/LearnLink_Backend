using LearnLink_Backend.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearnLink_Backend.Modules.Authentcation.Services.Token
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(UniversalUser user)
        {
            var handler = new JwtSecurityTokenHandler();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var PrivateKey = config.GetSection("PrivateKey").Value;

            //uses private key to start the token
            var key = Encoding.ASCII.GetBytes(PrivateKey!);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            //describes the attributes inside the payload 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(UniversalUser user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim("id", user.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            return claims;
        }
    }
}
