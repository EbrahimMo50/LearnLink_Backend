using LearnLink_Backend.Models;

namespace LearnLink_Backend.Modules.Post
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string? ImagePath { get; set; }
        public List<Student> Likes { get; set; }
    }
}