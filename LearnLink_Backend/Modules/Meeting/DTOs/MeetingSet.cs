using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Modules.Meeting.DTOs
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
}
