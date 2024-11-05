using LearnLink_Backend.Models;

namespace LearnLink_Backend.DTOs
{
    public class UniversalUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public static UniversalUser ToUser(Admin a)
        {
            return new UniversalUser { Id = a.Id, Email = a.Email, Name = a.Name};
        }

        public static UniversalUser ToUser(Student s)
        {
            return new UniversalUser { Id = s.Id, Email = s.Email, Name = s.Name};
        }

        public static UniversalUser ToUser(Instructor i)
        {
            return new UniversalUser { Id = i.Id, Email = i.Email, Name = i.Name};
        }

    }

}
