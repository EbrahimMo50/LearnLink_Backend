using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Notification
{
    public class AnnouncementModel
    {
        public string Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
