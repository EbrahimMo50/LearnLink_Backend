using LearnLink_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Entities
{
    //a set up meeting between stuent and instructor
    public class MeetingModel
    {
        public int Id { get; }
        public string StudentId { get; set; } = string.Empty;
        public Student Student { get; set; } = null!;
        public string InstructorId { get; set; } = string.Empty;
        public Instructor Instructor { get; set; } = null!;
        public DateTime StratDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}