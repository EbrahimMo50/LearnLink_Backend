using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Announcement.DTOs
{
    public class AnnouncementGet
    {
        public string Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
    }
}
