using LearnLink_Backend.Modules.Post.DTOs;

namespace LearnLink_Backend.Modules.Post.Repos
{
    public interface IPostRepo
    {
        public Task<PostModel> CreatePost(PostModel post);
        public PostModel GetPost(int id);
        public Task<IEnumerable<PostModel>> GetRecentPosts(int limit, int page);
        public void DeleteTask(int id);
        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId);
    }
}