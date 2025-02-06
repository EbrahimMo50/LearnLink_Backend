using LearnLink_Backend.Modules.Posts;

namespace LearnLink_Backend.Modules.Posts.Repos
{
    public interface IPostRepo
    {
        public Task<PostModel> CreatePostAsync(PostModel post);
        public PostModel? GetPostById(int id);
        public Task<IEnumerable<PostModel>> GetRecentPostsAsync(int limit, int page);
        public void DeletePost(int id);
        public PostModel UpdatePost(PostModel post);
    }
}