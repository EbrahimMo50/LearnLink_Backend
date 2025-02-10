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
    public class InstructorGet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal FeesPerHour { get; set; }
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }

        public static InstructorGet ToDTO(Models.Instructor instructor)
        {
            return new() { Email = instructor.Email, FeesPerHour = instructor.FeesPerHour, Name = instructor.Name, Id = instructor.Id.ToString(), Nationality = instructor.Nationality, SpokenLanguage = instructor.SpokenLanguage };
        }

        public static IEnumerable<InstructorGet> ToDTO(IEnumerable<Models.Instructor> instructors)
        {
            List<InstructorGet> result = [];
            foreach (var instructor in instructors)
                result.Add(ToDTO(instructor));
            return result;
        }
        
    }
}
