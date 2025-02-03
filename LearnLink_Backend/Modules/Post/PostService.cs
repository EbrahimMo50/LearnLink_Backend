using LearnLink_Backend.Modules.Post.DTOs;
using LearnLink_Backend.Modules.Post.Repos;

namespace LearnLink_Backend.Modules.Post
{
    public class PostService(IPostRepo repo)
    {

        internal object CreatePost(PostSet post, string IssuerId)
        {
            throw new NotImplementedException();
        }

        internal object GetPost(int id)
        {
            throw new NotImplementedException();
        }

        internal object GetRecentPosts(int limit, int page)
        {
            throw new NotImplementedException();
        }
    }
}