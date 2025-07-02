using LearnLink_Backend.DTOs;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Authentcation.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }

        [StringLength(16, MinimumLength = 4)]
        public string Password { get; set; }
    }
    public class LoginSuccessDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public UniversalUser User { get; set; } = null!;
    }
}
