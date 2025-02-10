using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Repositories.PostsRepo
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