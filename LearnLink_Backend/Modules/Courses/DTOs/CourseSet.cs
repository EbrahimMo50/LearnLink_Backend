using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Session;

namespace LearnLink_Backend.Modules.Courses.DTOs
{
    public class CourseSet
    {
        public string Name { get; set; }
        public int InstructorId { get; set; }
    }
}
