using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Session.DTOs
{
    public class SessionSet
    {
        public string MeetingLink { get; set; }
        public int CourseId { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public DateOnly Day { get; set; }

    }
}
