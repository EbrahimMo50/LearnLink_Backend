using LearnLink_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class AnnouncementGet
    {
        public int Id { get; set; }
        [MinLength(4)]
        public string Title { get; set; } = string.Empty;
        [MinLength(4)]
        public string Description { get; set; } = string.Empty;
        public int CourseId { get; set; }

        public static AnnouncementGet ToDTO(AnnouncementModel announcement)
        {
            return new AnnouncementGet() 
            { 
                Id = announcement.Id, 
                Title = announcement.Title, 
                Description = announcement.Description,
                CourseId = announcement.CourseId 
            };
        }

        public static IEnumerable<AnnouncementGet> ToDTO(IEnumerable<AnnouncementModel> announcements)
        {
            var list = new List<AnnouncementGet>();

            foreach (var announcementItem in announcements)
                list.Add(ToDTO(announcementItem));

            return list;
        }
    }
    public class AnnouncementSet
    {
        [MinLength(4)]
        public string Title { get; set; } = string.Empty;
        [MinLength(4)]
        public string Description { get; set; } = string.Empty;
    }
    public class AnnouncementUpdate
    {
        [MinLength(4)]
        public string Title { get; set; } = string.Empty;
        [MinLength(4)] 
        public string Description { get; set; } = string.Empty;
    }
}
