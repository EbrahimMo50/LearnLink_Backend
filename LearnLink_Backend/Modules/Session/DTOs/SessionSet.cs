using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Session.DTOs
{
    public class SessionSet
    {
        public string MeetingLink { get; set; }
        private int CourseId;
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public DateOnly Day { get; set; }
        public int GetCourseId()
        {
            return CourseId;
        }
        public void SetCourseId(int courseId)
        {
            CourseId = courseId;
        }
    }
}
