using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.DTOs.InstructorDTOs
{
    public class InstructorGet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        
        public static InstructorGet ToDTO(Instructor instructor)
        {
            return new() { Email = instructor.Email, FeesPerHour = instructor.FeesPerHour, Name = instructor.Name, Id = instructor.Id.ToString(), Nationality = instructor.Nationality, SpokenLanguage = instructor.SpokenLanguage };
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
