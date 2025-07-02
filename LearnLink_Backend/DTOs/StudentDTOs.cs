using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class StudentSet
    {
        [MinLength(4)]
        public string Name { get; set; } = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [MinLength(4)]
        public string Nationality { get; set; } = string.Empty;
        [MinLength(4)]
        public IEnumerable<string> SpokenLanguages { get; set; } = [];
        [MinLength(8)]
        public string Address { get; set; } = string.Empty;

        public Models.Student ToStudent(string hashedPassword, string salt)
        {
            return new Models.Student()
            {
                Name = this.Name,
                Email = this.Email,
                Nationality = this.Nationality,
                SpokenLanguages = this.SpokenLanguages.ToList(),
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
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public IEnumerable<string> SpokenLanguages { get; set; } = [];
        public string Address { get; set; } = string.Empty;

        public static StudentGet ToDTO(Models.Student student)
        {
            return new StudentGet() 
            {
                Id = student.Id.ToString(),
                Name = student.Name,
                Balance = student.Balance,
                Email = student.Email, 
                Nationality = student.Nationality,
                SpokenLanguages = student.SpokenLanguages, 
                Address = student.Address 
            };
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
