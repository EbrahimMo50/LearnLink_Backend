using LearnLink_Backend.Modules.Posts.DTOs;

namespace LearnLink_Backend.Modules.Posts.Services
{
    public interface IPostService
    {
        public Task<PostModel> CreatePostAsync(PostSet postSet, string IssuerId);
        public PostGet GetPost(int id);
        public Task<IEnumerable<PostGet>> GetRecentPostsAsync(int limit, int page);
        public void DeletePost(int id);
        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId);
    }
}
