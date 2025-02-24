using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class StudentSet
    {
        [MinLength(4)]
        public string Name { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [MinLength(4)]
        public string Nationality { get; set; }
        [MinLength(4)]
        public string SpokenLanguage { get; set; }
        [MinLength(8)]
        public string Address { get; set; }

        public Models.Student ToStudent(string hashedPassword, string salt)
        {
            return new Models.Student()
            {
                Name = this.Name,
                Email = this.Email,
                Nationality = this.Nationality,
                SpokenLanguage = this.SpokenLanguage,
                Address = this.Address,
                HashedPassword = hashedPassword,
                Salt = salt,
                Balance = 0.0M,
                AtDate = DateTime.Now,
                UpdateTime = DateTime.Now
            };
        }
    }
    public class StudentGet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public string Address { get; set; }

        public static StudentGet ToDTO(Models.Student student)
        {
            return new StudentGet() { Id = student.Id.ToString(), Name = student.Name, Balance = student.Balance, Email = student.Email, Nationality = student.Nationality, SpokenLanguage = student.SpokenLanguage, Address = student.SpokenLanguage };
        }
        public static IEnumerable<StudentGet> ToDTO(IEnumerable<Models.Student> student)
        {
            List<StudentGet> result = [];

            foreach (var item in student)
                result.Add(ToDTO(item));

            return result;
        }
    }
}
