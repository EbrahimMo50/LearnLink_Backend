using LearnLink_Backend.Modules.Courses.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    //shall i add the instructor scehdule here?
    public class Instructor
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public decimal FeesPerHour { get; set; } = 0;   //this is meeting releated
        public string Nationality { get; set; }
        public string SpokenLanguage { get; set; }
        public List<CourseModel> Courses { get; set; } = [];
        public virtual Schedule? Schedule { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class Schedule
    {        
        public int Id { get; }
        public Guid InstructorId { get; set; }
        [Range(0,6)]
        public List<int> AvilableDays { get; set; } = []; // this will have values from 0 to 6
        [Range(0,24)]
        public int StartHour { get; set; }
        [Range(0,24)]
        public int EndHour { get; set; }                  // from 0 to 24
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
