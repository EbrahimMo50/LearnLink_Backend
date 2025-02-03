using LearnLink_Backend.Models;

namespace LearnLink_Backend.Modules.Post.DTOs
{
    public class PostGet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string? ImagePath { get; set; } = null;  //nullable because not all posts have images
        public List<string> Likes { get; set; }
    }
}