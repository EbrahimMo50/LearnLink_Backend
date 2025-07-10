using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Models;

namespace LearnLink_Backend.Repositories.PostsRepo
{
    public interface IPostRepo
    {
        public Task<PostModel> CreatePostAsync(PostModel post);
        public PostModel? GetPostById(int id);
        public Task<IEnumerable<PostGet>> GetRecentPostsAsync(int limit, int page);
        public int GetPostCount();
        public void ReactToPost(PostModel post, Student user);
        public void RemoveReact(PostModel post, Student user);
        public void DeletePost(int id);
        public PostModel UpdatePost(PostModel post);
        public IEnumerable<Comment> GetAllComments(int postId);
        public Comment? GetCommentById(int id);
        public Comment AddComment(Comment comment);
    }
}