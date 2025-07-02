using LearnLink_Backend.Models;

namespace LearnLink_Backend.Entities
{
    public class CourseModel
    {
        public int Id { get; }
        public string Name { get; set; } = string.Empty;
        public Instructor? Instructor { get; set; }
        public List<SessionModel> Sessions { get; set; } = [];
        public List<AnnouncementModel> Announcements { get; set; } = [];
        public List<Student> Students { get; set; } = [];
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
