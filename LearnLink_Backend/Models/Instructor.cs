using LearnLink_Backend.Modules.Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    //shall i add the instructor scehdule here?
    public class Instructor
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public List<CourseModel> Courses { get; set; } = new List<CourseModel>();
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
