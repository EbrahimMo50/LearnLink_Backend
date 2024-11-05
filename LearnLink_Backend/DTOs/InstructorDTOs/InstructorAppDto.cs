using System.ComponentModel.DataAnnotations.Schema;

namespace LearnLink_Backend.DTOs.InstructorDTOs
{
    public class InstructorAppDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Messsage { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
