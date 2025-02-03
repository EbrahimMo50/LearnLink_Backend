using LearnLink_Backend.Models;

namespace LearnLink_Backend.Modules.Post.DTOs
{
    public class PostSet
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }    //will be added in presentation layer from http context
        public string? ImageName { get; set; }
    }
}
