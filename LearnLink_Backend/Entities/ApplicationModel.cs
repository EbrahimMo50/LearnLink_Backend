using System;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Entities
{
    public class ApplicationModel
    {
        public int Id { get; }
        public string Name { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Messsage { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public ICollection<string> SpokenLanguages { get; set; } = [];
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}