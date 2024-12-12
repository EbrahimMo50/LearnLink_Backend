using LearnLink_Backend.Models;

namespace LearnLink_Backend.Modules.Meeting
{
    //a set up meeting between stuent and instructor
    public class MeetingModel
    {
        public int Id { get; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
