using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Session;

namespace LearnLink_Backend.Modules.Courses.DTOs
{
    public class CourseGet
    {
        public int Id { get; }
        public string Name { get; set; }
        public Instructor Instructor { get; set; }
    }
}
