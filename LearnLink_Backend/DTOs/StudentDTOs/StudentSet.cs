

namespace LearnLink_Backend.DTOs.StudentDTOs
{
    public class StudentSet
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
