using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Authentcation.DTOs
{
    public class ChangePassDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }

        [StringLength(16, MinimumLength = 4)]
        public string NewPassword { get; set; }
    }
}