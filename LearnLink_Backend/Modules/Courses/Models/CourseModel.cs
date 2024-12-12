using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Notification;
using LearnLink_Backend.Modules.Session;

namespace LearnLink_Backend.Modules.Courses.Models
{
    public class CourseModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public Instructor? Instructor { get; set; }
        public List<SessionModel> Sessions { get; set; } = new List<SessionModel>();
        public List<AnnouncementModel> Announcements { get; set; } = new List<AnnouncementModel>();
        public List<Student> Students { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
