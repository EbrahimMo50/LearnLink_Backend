using LearnLink_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class MeetingSet
    {
        public string StudentId { get; set; }
        public string InstructorId { get; set; }
        [Range(0, 6)]
        public int Day { get; set; }           // 1:7 day
        [Range(0, 24)]
        public int StartsAt { get; set; }       // hour -> hour with 0 to 24 (hourly precision)
        [Range(0, 24)]
        public int EndsAt { get; set; }
    }

    public class MeetingGet
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string InstructorId { get; set; }
        public int Day { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }
        public static MeetingGet ToDTO(MeetingModel meeting)
        {
            return new MeetingGet() { Id = meeting.Id, Day = meeting.Day, StudentId = meeting.StudentId, InstructorId = meeting.InstructorId, EndsAt = meeting.EndsAt, StartsAt = meeting.StartsAt };
        }
        public static IEnumerable<MeetingGet> ToDTO(IEnumerable<MeetingModel> meetings)
        {
            List<MeetingGet> result = [];
            foreach (MeetingModel meeting in meetings)
                result.Add(ToDTO(meeting));
            return result;
        }
    }
}
