using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Models;

namespace LearnLink_Backend.Services.PostsService
{
    public interface IPostService
    {
        public Task<PostModel> CreatePostAsync(PostSet postSet, string IssuerId);
        public PostGet GetPost(int id);
        public Task<PaggedModel<PostGet>> GetRecentPostsAsync(int limit, int page);
        public void DeletePost(int id);
        public int ReactToPost(int id, string userId);
        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId);
        public Comment GetComment(int id);
        public IEnumerable<Comment> GetAllComments(int postId);
        public Comment AddComment(CommentDto commentDto);
    }
}
