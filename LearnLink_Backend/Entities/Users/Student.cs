using LearnLink_Backend.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public ICollection<string> SpokenLanguages { get; set; } = [];
        public string Address { get; set; } = string.Empty;
        public ICollection<CourseModel> Courses { get; set; } = [];
        public ICollection<PostModel> LikedPosts { get; set; } = [];
        public ICollection<SessionModel> Sessions { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
