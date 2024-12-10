using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Session
{
    public class SessionModel
    {
        public int Id { get; }
        public Instructor Instructor { get; set; }
        public List<Student> AttendendStudent { get; set; } = new List<Student>();
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public DateOnly Day { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        

        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
