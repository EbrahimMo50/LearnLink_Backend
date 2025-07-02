using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class InstructorGet
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public IEnumerable<string> SpokenLanguages { get; set; } = [];

        public static InstructorGet ToDTO(Instructor instructor)
        {
            return new() { 
                Email = instructor.Email, 
                FeesPerHour = instructor.FeesPerHour, 
                Name = instructor.Name, Id = instructor.Id.ToString(),
                Nationality = instructor.Nationality, 
                SpokenLanguages = instructor.SpokenLanguages
            };
        }

        public static IEnumerable<InstructorGet> ToDTO(IEnumerable<Instructor> instructors)
        {
            List<InstructorGet> result = [];
            foreach (var instructor in instructors)
                result.Add(ToDTO(instructor));
            return result;
        }
        
    }
}
