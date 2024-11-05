using System.ComponentModel.DataAnnotations.Schema;

namespace LearnLink_Backend.Models
{
    public class Meeting
    {
        public int Id { get; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AtDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
