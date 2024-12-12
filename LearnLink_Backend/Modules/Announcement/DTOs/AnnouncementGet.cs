using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Announcement;

namespace LearnLink_Backend.Modules.Announcement.DTOs
{
    public class AnnouncementGet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }

        public static AnnouncementGet ToDTO(AnnouncementModel announcement)
        {
            return new AnnouncementGet() { Id = announcement.Id, Title = announcement.Title, Description = announcement.Description, CourseId = announcement.CourseId };
        }

        public static IEnumerable<AnnouncementGet> ToDTO(IEnumerable<AnnouncementModel> announcements)
        {
            var list = new List<AnnouncementGet>();

            foreach (var announcementItem in announcements)
                list.Add(ToDTO(announcementItem));

            return list;
        }
    }
}
