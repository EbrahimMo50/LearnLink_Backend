using LearnLink_Backend.Entities;

namespace LearnLink_Backend.DTOs
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
    public class SessionGet
    {
        public int Id { get; set; }
        public string MeetingLink { get; set; }
        public IEnumerable<string> AttendendStudent { get; set; } = [];
        public int CourseId { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
        public DateOnly Day { get; set; }

        internal static IEnumerable<SessionGet> ToDTO(IEnumerable<SessionModel> enumerable)
        {
            List<SessionGet> result = [];
            foreach (SessionModel model in enumerable)
            {
                result.Add(ToDTO(model));
            }
            return result;
        }

        internal static SessionGet ToDTO(SessionModel session)
        {
            return new SessionGet
            {
                Id = session.Id,
                MeetingLink = session.MeetingLink,
                AttendendStudent = session.AttendendStudent.Select(x => x.Id).Select(x => x.ToString()),
                CourseId = session.Course.Id,
                StartsAt = session.StartsAt,
                EndsAt = session.EndsAt,
                Day = session.Day
            };
        }
    }
}
