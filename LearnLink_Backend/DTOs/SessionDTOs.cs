using LearnLink_Backend.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs
{
    public class SessionSet
    {
        [MinLength(4)]
        public string? MeetingLink { get; set; } = null;
        [JsonIgnore]
        [BindNever]
        public int CourseId { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class SessionGet
    {
        public int Id { get; set; }
        public string? MeetingLink { get; set; } = string.Empty;
        public IEnumerable<string> AttendendStudent { get; set; } = [];
        public int CourseId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
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
                StartTime = session.StartTime,
                EndTime = session.EndTime,
            };
        }
    }
}
