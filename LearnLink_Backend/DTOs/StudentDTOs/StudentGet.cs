using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.DTOs.StudentDTOs
{
    public class StudentGet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public string Address { get; set; }

        public static StudentGet ToDTO(Student student)
        {
            return new StudentGet() { Id = student.Id.ToString(), Name = student.Name, Balance = student.Balance, Email = student.Email, Nationality = student.Nationality, SpokenLanguage = student.SpokenLanguage, Address = student.SpokenLanguage };
        }
        public static IEnumerable<StudentGet> ToDTO(IEnumerable<Student> student)
        {
            List<StudentGet> result = [];

            foreach (var item in student)
                result.Add(ToDTO(item));
                
           return result;
        }
    }
}