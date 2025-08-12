using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Repositories.PostsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;

namespace LearnLink_Backend.Services.PostsService
{
    public class PostService(IPostRepo postRepo, IUserRepo userRepo) : IPostService
    {
        public async Task<PostModel> CreatePostAsync(PostSet postSet, string IssuerId)
        {
            PostModel post = new();
            Admin admin = userRepo.GetAdminById(IssuerId) ?? throw new NotFoundException("Admin not found");
            post.Author = admin;
            post.Title = postSet.Title;
            post.Description = postSet.Description;
            post.CreatedBy = IssuerId;
            post.ImagePath = postSet.ImageName;
            var result = await postRepo.CreatePostAsync(post);
            result.ImagePath = "https://localhost:7209/api/post/media/" + result.ImagePath;
            return result;
        }

        public PostGet GetPost(int id)
        {
            return PostGet.ToDTO(postRepo.GetPostById(id) ?? throw new NotFoundException("could not find post"));
        }

        public async Task<PaggedModel<PostGet>> GetRecentPostsAsync(int limit, int page)
        {
            var posts = await postRepo.GetRecentPostsAsync(limit, page);

            foreach(var post in posts)
                if(post.MediaLink != "" && post.MediaLink is not null)
                    post.MediaLink = $"https://localhost:7209/api/post/media/{post.MediaLink}";
            
            
            var postCount = postRepo.GetPostCount();
            return new PaggedModel<PostGet>(posts, postCount, page, posts.Count());
        }

        public void DeletePost(int id)
        {
            postRepo.DeletePost(id);
        }

        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId)
        {
            if (userRepo.GetAdminById(IssuerId) == null)
                throw new NotFoundException("Admin not found");

            var post = postRepo.GetPostById(id) ?? throw new NotFoundException("Post not found");

            post.Description = newPost.Description;
            post.Title = newPost.Title;
            post.ImagePath = newPost.ImageName;
            post.UpdatedBy = IssuerId;
            post.UpdateTime = DateTime.UtcNow;
            return postRepo.UpdatePost(post);
        }

        public CommentGet GetComment(int id)
        {
            return CommentGet.ToDto(postRepo.GetCommentById(id)) ?? throw new NotFoundException("comment not found");
        }

        public IEnumerable<CommentGet> GetAllComments(int postId)
        {
            return CommentGet.ToDto(postRepo.GetAllComments(postId));
        }

        public Comment AddComment(CommentSet CommentSet)
        {  
            PostModel post = postRepo.GetPostById(CommentSet.PostId) ?? throw new NotFoundException("post not found");
            Student user = userRepo.GetStudentById(CommentSet.UserGuid) ?? throw new NotFoundException("usre not defined");
            return postRepo.AddComment(new Comment() { Content = CommentSet.Content, Post = post, Commenter = user });
        }

        public int ReactToPost(int id, string userId)
        {
            var post = postRepo.GetPostById(id) ?? throw new NotFoundException("post not found");
            var user = post.Likes.FirstOrDefault(p => p.Id.ToString() == userId);
            if (user == null)
            {
                postRepo.ReactToPost(post, userRepo.GetStudent(userId)!);
                return post.Likes.Count;
            }
            else
            {
                postRepo.RemoveReact(post, user);
                return post.Likes.Count;
            }
        }
    }
}
