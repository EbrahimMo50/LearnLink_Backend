using LearnLink_Backend.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnLink_Backend.DTOs
{
    public class PostSet
    {
        [MinLength(4)]
        public string Title { get; set; } = string.Empty;
        [MinLength(10)]
        public string Description { get; set; } = string.Empty;
        [BindNever]
        [JsonIgnore]
        public string? AuthorId { get; set; }    //will be added in presentation layer from http context
        public IFormFile? Image { get; set; } = null;
        [BindNever]
        [JsonIgnore]
        public string? ImageName { get; set; } = null;
    }
    public class PostGet
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = null;  //nullable because not all posts have images
        public IEnumerable<string> Likes { get; set; } = [];

        public static PostGet ToDTO(PostModel post)
        {
            return new PostGet 
            { 
                Id = post.Id, 
                Title = post.Title, 
                Description = post.Description, 
                AuthorId = post.Author.Id.ToString(),
                ImagePath = post.ImagePath,
                Likes = post.Likes.Select(x => x.Id.ToString()).ToList() 
            };
        }

        public static IEnumerable<PostGet> ToDTO(IEnumerable<PostModel> posts)
        {
            return posts.Select(x => ToDTO(x));
        }
    }
}
