using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;
//class for mimicing polymorphism between users

namespace LearnLink_Backend.DTOs
{
    public class UniversalUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Role { get; set; }
        public static UniversalUser ToUser(Admin a)
        {
            return new UniversalUser { Id = a.Id, Email = a.Email, Name = a.Name, Role = "Admin"};
        }

        public static UniversalUser ToUser(Models.Student s)
        {
            return new UniversalUser { Id = s.Id, Email = s.Email, Name = s.Name, Role = "Student"};
        }

        public static UniversalUser ToUser(Models.Instructor i)
        {
            return new UniversalUser { Id = i.Id, Email = i.Email, Name = i.Name, Role = "Instructor"};
        }

    }

}
