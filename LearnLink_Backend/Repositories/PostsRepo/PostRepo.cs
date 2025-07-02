using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.PostsRepo
{
    public class PostRepo(AppDbContext dbContext) : IPostRepo
    {
        public Comment AddComment(Comment comment)
        {
            var newComment = dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
            return newComment.Entity;
        }

        public async Task<PostModel> CreatePostAsync(PostModel post)
        {
            var result = await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void DeletePost(int id)
        {
            dbContext.Posts.Where(x => x.Id == id).ExecuteDelete();     //first time trying this method of deleting
        }

        public IEnumerable<Comment> GetAllComments(int postId)
        {
            return [.. dbContext.Comments];
        }

        public Comment? GetCommentById(int id)
        {
            return dbContext.Comments.SingleOrDefault(c => c.Id == id);
        }

        public PostModel? GetPostById(int id)
        {
            var result = dbContext.Posts.Where(x => x.Id == id).FirstOrDefault() ?? throw new NotFoundException("Post not found");
            return result;
        }

        public async Task<IEnumerable<PostModel>> GetRecentPostsAsync(int limit, int page)
        {
            var posts = await dbContext.Posts.Include(x => x.Likes).Include(x => x.Author).ToListAsync();
            var result = posts.AsEnumerable().Reverse().Skip((page - 1) * limit).Take(limit);
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
