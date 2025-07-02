using LearnLink_Backend.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs;
public class ScheduleUpdate
{
    [JsonIgnore]
    [BindNever]
    public string InstructorId { get; set; } = string.Empty;
    public ICollection<DayAvailability> NewSchedule { get; set; } = [];
}