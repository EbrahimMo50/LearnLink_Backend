using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Authentcation.DTOs
{
    public class ChangePassDTO
    {
        public string Email { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;

        [StringLength(16, MinimumLength = 4)]
        public string NewPassword { get; set; } = string.Empty;
    }
}