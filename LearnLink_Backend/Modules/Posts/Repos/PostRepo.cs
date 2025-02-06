using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Posts.Repos
{
    public class PostRepo(AppDbContext dbContext) : IPostRepo
    {
        public async Task<PostModel> CreatePostAsync(PostModel post)
        {
            var result = await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeletePost(int id)
        {
            dbContext.Posts.Where(x=> x.Id == id).ExecuteDelete();     //first time trying this method of deleting
        }

        public PostModel? GetPostById(int id)
        {
            var result = dbContext.Posts.Where(x => x.Id == id).FirstOrDefault() ?? throw new NotFoundException("Post not found");
            return result;
        }

        public async Task<IEnumerable<PostModel>> GetRecentPostsAsync(int limit, int page)
        {
            var result = await dbContext.Posts.Reverse().Skip((page - 1) * limit).Take(limit).ToListAsync();
            return result;
        }

        public PostModel UpdatePost(PostModel post)
        {
            var result = dbContext.Posts.Update(post);
            dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
