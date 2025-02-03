using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Post.DTOs;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Post.Repos
{
    public class PostRepo(AppDbContext DbContext) : IPostRepo
    {
        public async Task<PostModel> CreatePost(PostModel post)
        {
            await DbContext.Posts.AddAsync(post);
            await DbContext.SaveChangesAsync();
            return post;
        }

        public void DeleteTask(int id)
        {
            DbContext.Posts.Where(x=> x.Id == id).ExecuteDelete();     //first time trying this method of deleting
        }

        public PostModel GetPost(int id)
        {
            var result = DbContext.Posts.Where(x => x.Id == id).FirstOrDefault() ?? throw new NotFoundException("Post not found");
            return result;
        }

        public async Task<IEnumerable<PostModel>> GetRecentPosts(int limit, int page)
        {
            var result = await DbContext.Posts.Skip((page - 1) * limit).Take(limit).ToListAsync();
            return result;
        }

        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId)
        {
            var post = DbContext.Posts.Where(x => x.Id == id).FirstOrDefault() ?? throw new NotFoundException("Post not found");
            post.Title = newPost.Title;
            post.Description = newPost.Description;
            post.UpdatedBy = IssuerId;
            post.UpdateTime = DateTime.Now;
            //author will remain unchanged
            DbContext.SaveChanges();
            return post;
        }
    }
}
