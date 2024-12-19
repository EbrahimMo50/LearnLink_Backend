using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Session.DTOs
{
    public class SessionGet
    {
        public int Id { get; }
        public string MeetingLink { get; set; }
        public Instructor Instructor { get; set; }
        public List<Student> AttendendStudent { get; set; } = [];
        public CourseModel Course { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public DateOnly Day { get; set; }
    }
}
