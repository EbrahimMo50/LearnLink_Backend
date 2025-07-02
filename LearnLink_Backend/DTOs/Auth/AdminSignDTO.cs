using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Adminstration.DTOs
{
    public class AdminSignDTO
    {
        public string Name { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
