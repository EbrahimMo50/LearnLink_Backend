using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.DTOs
{
    public class InstructorScheduleGet
    {
        public string InstructorId { get; set; }
        [Range(0, 6)]
        public List<int> AvilableDays { get; set; } = []; // this will have values from 0 to 6
        [Range(0, 24)]
        public int StartHour { get; set; }
        [Range(0, 24)]
        public int EndHour { get; set; }                  // from 0 to 24

        public static InstructorScheduleGet ToDTO(Schedule schedule)
        {
            return new InstructorScheduleGet
            {
                InstructorId = schedule.InstructorId.ToString(),
                AvilableDays = schedule.AvilableDays,
                StartHour = schedule.StartHour,
                EndHour = schedule.EndHour
            };
        }

        public static IEnumerable<InstructorScheduleGet> ToDTO(IEnumerable<Schedule> schedules)
        {
            return schedules.Select(x => ToDTO(x));
        }
    }
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
