namespace LearnLink_Backend.Modules.Meeting.DTOs
{
    public class ScheduleSet
    {
        public string InstructorId { get; set; }
        public List<int> AvilableDays { get; set; } = []; // this will have values from 0 to 6
        public int StartHour { get; set; }
        public int EndHour { get; set; }                  // from 0 to 24
    }
}
