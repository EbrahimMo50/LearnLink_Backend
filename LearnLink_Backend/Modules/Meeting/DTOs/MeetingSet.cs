namespace LearnLink_Backend.Modules.Meeting.DTOs
{
    public class MeetingSet
    {
        public string StudentId { get; set; }
        public string InstructorId { get; set; }
        public int Day { get; set; }           // 1:7 day
        public int StartsAt { get; set; }       // hour -> hour with 0 to 24 (hourly precision)
        public int EndsAt { get; set; }
    }
}
