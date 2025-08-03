using LearnLink_Backend.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs;
public class ScheduleUpdate : IValidatableObject
{
    [JsonIgnore]
    [BindNever]
    public string InstructorId { get; set; } = string.Empty;
    public ICollection<DayAvailability> NewSchedule { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var day in NewSchedule)
        {
            foreach(var interval in day.Intervals)
            {
                if (interval.Start > interval.End)
                {
                    yield return new ValidationResult($"Start time {interval.Start} can not be after end time {interval.End}",
                        [nameof(NewSchedule)]);
                }
                Console.WriteLine((interval.End - interval.Start).Minutes);
                if ((interval.End - interval.Start).TotalMinutes < 30)
                {
                    yield return new ValidationResult("minimum meeting time is 30 minutes", [nameof(NewSchedule)]);
                }
            }
        }
    }
}