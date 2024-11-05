using System.ComponentModel.DataAnnotations.Schema;

namespace LearnLink_Backend.Models
{
    public class Schedule
    {
        public int Id { get; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public DateOnly Day {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AtDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
