using LearnLink_Backend.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearnLink_Backend.Models
{
    [Index(nameof(Email), IsUnique = true)]
    //shall i add the instructor scehdule here?
    public class Instructor
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal FeesPerHour { get; set; } = 0;   //this is meeting releated
        public string Nationality { get; set; } = string.Empty;
        public ICollection<string> SpokenLanguages { get; set; } = [];
        public ICollection<CourseModel> Courses { get; set; } = [];
        public ICollection<DayAvailability> Schedule { get; set; } = [];
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class DayAvailability : IValidatableObject
    {
        public DayOfWeek Day { get; set; }
        public ICollection<TimeInterval> Intervals { get; set; } = [];
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var interval in Intervals)
            {
                if (interval.Start > interval.End)
                {
                    yield return new ValidationResult(
                        $"Start time {interval.Start} cannot be after end time {interval.End}.",
                        [nameof(Intervals)]);
                }
            }
        }
    }
    public class TimeInterval
    {
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
}
