using LearnLink_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class MeetingSet
    {
        public string StudentId { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class MeetingGet
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public static MeetingGet ToDTO(MeetingModel meeting)
        {
            return new MeetingGet() 
            {
                Id = meeting.Id,
                StudentId = meeting.StudentId, 
                InstructorId = meeting.InstructorId,
                StartDate = meeting.StratDate,
                EndDate = meeting.StratDate
            };
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
