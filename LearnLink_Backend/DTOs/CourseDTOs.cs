using LearnLink_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class CourseSet
    {
        [MinLength(4)]
        public string Name { get; set; }
        public string InstructorId { get; set; }
    }

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
