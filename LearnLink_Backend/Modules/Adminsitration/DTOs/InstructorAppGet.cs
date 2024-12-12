using LearnLink_Backend.Modules.Adminstration.Models;

namespace LearnLink_Backend.Modules.Adminstration.DTOs
{
    public class InstructorAppGet
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Messsage { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public static InstructorAppGet ToDTO(InstructorApplicationModel app)
        {
            return new InstructorAppGet { Name = app.Name, Email = app.Email, Messsage = app.Messsage, Nationality = app.Nationality, SpokenLanguage = app.Nationality };
        }

        public static IEnumerable<InstructorAppGet> ToDTO(IEnumerable<InstructorApplicationModel> app)
        {
            var result = new List<InstructorAppGet>();

            foreach (var appItem in app) 
                result.Add(ToDTO(appItem));

            return result;
        }
    }
}
