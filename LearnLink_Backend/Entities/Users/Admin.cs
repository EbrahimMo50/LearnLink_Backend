using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Admin
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
