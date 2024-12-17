using LearnLink_Backend.Models;

namespace LearnLink_Backend.Modules.Meeting
{
    //a set up meeting between stuent and instructor
    public class MeetingModel
    {
        public int Id { get; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public int Day { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
