using LearnLink_Backend.Models;

namespace LearnLink_Backend.Entities
{
    public class PostModel
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Admin Author { get; set; }
        public string? ImagePath { get; set; } = null;  //nullable because not all posts have images
        public List<Student> Likes { get; set; }
        public DateTime AtDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedBy { get; set; }
    }
}