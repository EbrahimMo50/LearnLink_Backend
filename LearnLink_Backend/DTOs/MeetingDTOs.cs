using LearnLink_Backend.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs
{
    public class MeetingSet : IValidatableObject
    {
        [BindNever]
        [JsonIgnore]
        public string StudentId { get; set; } = string.Empty;
        public string InstructorId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartDate > EndDate)
            {
                yield return new ValidationResult($"Start time {StartDate} can not be after end time {EndDate}", [nameof(StartDate), nameof(EndDate)]);
            }
            if ((EndDate - StartDate) < new TimeSpan(0,30,0))
            {
                yield return new ValidationResult("minimum meeting time is 30 minutes", [nameof(StartDate), nameof(EndDate)]);
            }
        }
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
                StartDate = meeting.StartDate,
                EndDate = meeting.EndDate
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
