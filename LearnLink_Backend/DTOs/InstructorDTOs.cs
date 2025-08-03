using LearnLink_Backend.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs
{
    public class InstructorGet
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public IEnumerable<string> SpokenLanguages { get; set; } = [];

        public static InstructorGet ToDTO(Instructor instructor)
        {
            return new() { 
                PhoneNumber = instructor.PhoneNumber,
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

    public class InstructorUpdate
    {
        [JsonIgnore]
        [BindNever]
        public string Id { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        [Range(1,100)]
        public decimal FeesPerHour { get; set; }
    }
}
