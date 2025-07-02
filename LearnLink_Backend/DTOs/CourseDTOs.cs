using LearnLink_Backend.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs
{
    public class CourseSet
    {
        [MinLength(4)]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        [BindNever]
        public string InstructorId { get; set; } = string.Empty;
    }

    public class CourseGet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;

        public static CourseGet ToDTO(CourseModel course)
        {
            return new CourseGet() 
            {
                Id = course.Id,
                InstructorId = course.Instructor!.Id.ToString(),
                Name = course.Name, 
                InstructorName = course.Instructor.Name 
            };
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
