using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs;

public class CommentDto
{
    [StringLength(8000, MinimumLength = 1)]
    public string Content { get; set; } = string.Empty;
    [BindNever]
    [JsonIgnore]
    public string UserGuid { get; set; } = string.Empty;
    [BindNever]
    [JsonIgnore]
    public int PostId { get; set; }
}