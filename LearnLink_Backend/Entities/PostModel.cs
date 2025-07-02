using LearnLink_Backend.Models;

namespace LearnLink_Backend.Entities
{
    public class PostModel
    {
        public int Id { get; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Admin Author { get; set; } = null!;
        public string? ImagePath { get; set; } = null;  //nullable because not all posts have images
        public List<Student> Likes { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        public Student Commenter { get; set; } = null!;
        public PostModel Post { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
    }
}