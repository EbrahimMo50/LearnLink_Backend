using LearnLink_Backend.Modules.Courses.Models;
using Microsoft.EntityFrameworkCore;


namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public string Address { get; set; }
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
