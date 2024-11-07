using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Instructor
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
