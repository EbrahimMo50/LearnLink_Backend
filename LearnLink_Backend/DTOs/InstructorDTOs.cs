using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class InstructorGet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }

        public static InstructorGet ToDTO(Models.Instructor instructor)
        {
            return new() { Email = instructor.Email, FeesPerHour = instructor.FeesPerHour, Name = instructor.Name, Id = instructor.Id.ToString(), Nationality = instructor.Nationality, SpokenLanguage = instructor.SpokenLanguage };
        }

        public static IEnumerable<InstructorGet> ToDTO(IEnumerable<Models.Instructor> instructors)
        {
            List<InstructorGet> result = [];
            foreach (var instructor in instructors)
                result.Add(ToDTO(instructor));
            return result;
        }
        
    }
}
