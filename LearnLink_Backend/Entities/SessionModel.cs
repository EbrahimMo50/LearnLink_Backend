using LearnLink_Backend.Models;

namespace LearnLink_Backend.Entities
{
    public class SessionModel
    {
        public int Id { get; }
        public string? MeetingLink { get; set; }
        public List<Student> AttendendStudent { get; set; } = [];
        public int CourseId { get; set; }
        public CourseModel Course { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
