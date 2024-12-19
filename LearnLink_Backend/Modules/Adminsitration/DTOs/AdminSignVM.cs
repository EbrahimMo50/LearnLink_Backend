using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Adminstration.DTOs
{
    public class AdminSignVM
    {
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
