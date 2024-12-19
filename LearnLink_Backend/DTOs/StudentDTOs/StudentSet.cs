

using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs.StudentDTOs
{
    public class StudentSet
    {
        public string Name { get; set; }
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public string Address { get; set; }

        public Student ToStudent(string hashedPassword, string salt)
        {
            return new Student() {
                Name = this.Name ,
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
        //test releated will be deleted in production
        public static StudentSet GenerateRandomStudent()
        {
            Random random = new();

            string[] names = { "Alice", "Bob", "Charlie", "David", "Eve" };
            string[] nationalities = { "American", "British", "Canadian", "French", "German" };
            string[] languages = { "English", "Spanish", "French", "German", "Mandarin" };
            string[] addresses = { "123 Main St", "456 Elm St", "789 Oak St", "101 Pine St", "202 Cedar St" };

            StudentSet student = new()
            {
                Name = names[random.Next(names.Length)],
                Password = Guid.NewGuid().ToString()[..8],
                Email = $"{names[random.Next(names.Length)]}@{names[random.Next(names.Length)]}.com",
                Nationality = nationalities[random.Next(nationalities.Length)],
                SpokenLanguage = languages[random.Next(languages.Length)],
                Address = addresses[random.Next(addresses.Length)],
            };

            return student;
        }
    }
}
