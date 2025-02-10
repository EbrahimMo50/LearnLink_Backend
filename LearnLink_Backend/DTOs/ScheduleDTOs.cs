using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class ScheduleSet
    {
        private string InstructorId;
        [Range(0, 6)]
        public List<int> AvilableDays { get; set; } = []; // this will have values from 0 to 6
        [Range(0, 24)]
        public int StartHour { get; set; }
        [Range(0, 24)]
        public int EndHour { get; set; }                  // from 0 to 24
        public void SetInstructorId(string id)
        {
            InstructorId = id;
        }
    }
}
