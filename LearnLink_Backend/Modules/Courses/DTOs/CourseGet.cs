using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Session;

namespace LearnLink_Backend.Modules.Courses.DTOs
{
    public class CourseGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }

        public static CourseGet ToDTO(CourseModel course)
        {
            return new CourseGet() { Id = course.Id, Instructor = course.Instructor!.Id.ToString(), Name = course.Name };
        }
        public static IEnumerable<CourseGet> ToDTO(IEnumerable<CourseModel> courses)
        {
            List<CourseGet> result = [];

            foreach (var course in courses) 
                result.Add(ToDTO(course));
            return result;
        }
    }
}