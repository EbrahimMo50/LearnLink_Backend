using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Post.DTOs;
using LearnLink_Backend.Modules.Post.Repos;
using LearnLink_Backend.Services;

namespace LearnLink_Backend.Modules.Post
{
    public class PostService(IPostRepo repo, MediaService mediaService, AppDbContext DbContext)
    {

        public async Task<PostModel> CreatePost(PostSet postSet, string IssuerId)
        {
            PostModel post = new();
            Admin admin = DbContext.Admins.Where(x => x.Id.ToString() == IssuerId).FirstOrDefault() ?? throw new NotFoundException("Admin not found");
            post.Author = admin;
            post.Title = postSet.Title;
            post.Description = postSet.Description;
            post.CreatedBy = IssuerId;
            post.ImagePath = postSet.ImageName;
            return await repo.CreatePost(post);
        }

        public PostGet GetPost(int id)
        {
            return PostGet.ToDTO(repo.GetPost(id));
        }

        public async Task<IEnumerable<PostGet>> GetRecentPosts(int limit, int page)
        {
            return PostGet.ToDTO(await repo.GetRecentPosts(limit, page));
        }

        public void DeleteTask(int id)
        {
            repo.DeleteTask(id);
        }

        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId)
        {
            if(DbContext.Admins.Where(x => x.Id.ToString() == IssuerId).FirstOrDefault() == null)
                throw new NotFoundException("Admin not found");

            if(DbContext.Posts.Where(x => x.Id == id).FirstOrDefault() == null)
                throw new NotFoundException("Post not found");

            return repo.UpdatePost(id, newPost, IssuerId);
        }
    }
}
