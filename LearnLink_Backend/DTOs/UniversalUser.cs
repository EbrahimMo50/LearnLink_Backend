using LearnLink_Backend.Models;
//class for mimicing polymorphism between users

namespace LearnLink_Backend.DTOs
{
    public class UniversalUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public static UniversalUser ToUser(Admin a)
        {
            return new UniversalUser { Id = a.Id, Email = a.Email, Name = a.Name, Role = "Admin"};
        }

        public static UniversalUser ToUser(Student s)
        {
            return new UniversalUser { Id = s.Id, Email = s.Email, Name = s.Name, Role = "Student"};
        }

        public static UniversalUser ToUser(Instructor i)
        {
            return new UniversalUser { Id = i.Id, Email = i.Email, Name = i.Name, Role = "Instructor"};
        }

    }

}
