using LearnLink_Backend.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class ApplicationSet
    {
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [MinLength(10)]
        public string Messsage { get; set; } = string.Empty;
        [MinLength(4)]
        public string Nationality { get; set; } = string.Empty;
        [MinLength(4)]
        public IEnumerable<string> SpokenLanguages { get; set; } = [];
    }
    public class ApplicationGet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Messsage { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public IEnumerable<string> SpokenLanguages { get; set; } = [];
        public static ApplicationGet ToDTO(ApplicationModel app)
        {
            return new ApplicationGet 
            {
                Id = app.Id,
                Name = app.Name, 
                Email = app.Email, 
                Messsage = app.Messsage,
                Nationality = app.Nationality,
                SpokenLanguages = app.SpokenLanguages
            };
        }

        public static IEnumerable<ApplicationGet> ToDTO(IEnumerable<ApplicationModel> app)
        {
            var result = new List<ApplicationGet>();

            foreach (var appItem in app)
                result.Add(ToDTO(appItem));

            return result;
        }
    }
}
