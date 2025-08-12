using LearnLink_Backend.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs;

public class CommentSet
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

public class CommentGet
{
    public int Id { get; set; }
    public string CommenterId { get; set; } = string.Empty;
    public string CommenterName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public static CommentGet ToDto(Comment comment)
    {
        return new CommentGet { Id = comment.Id, CommenterId = comment.Commenter.Id.ToString(), CommenterName = comment.Commenter.Name, Content = comment.Content };
    }

    public static IEnumerable<CommentGet> ToDto(IEnumerable<Comment> comment)
    {
        var list = new List<CommentGet>();
        foreach (var commentItem in comment)
        {
            list.Add(ToDto(commentItem));
        }
        return list;
    }
}