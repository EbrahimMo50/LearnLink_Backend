using LearnLink_Backend.Modules.Posts.DTOs;
using LearnLink_Backend.Modules.Posts.Services;
using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostService postService, IHttpContextAccessor httpContextAccess, MediaService mediaService) : ControllerBase
    {
        [HttpPost()]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreatePost(PostSet post)
        {
            bool fileExists = true;
            if (Request.Form.Files.Count == 0)
                fileExists = false;

            var file = Request.Form.Files[0]; 

            if (file.Length == 0)
                fileExists = false;
            
            string? imageName = null;

            if (fileExists)
                imageName = await mediaService.SaveImage(file);

            post.ImageName = imageName;

            string? IssuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (IssuerId == null)
                return BadRequest("could not extract issuer id from http context");

            var result = postService.CreatePostAsync(post, IssuerId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetPost(int id)
        {
            var result = postService.GetPost(id);
            return Ok(result);
        }
        [HttpGet("recent")]
        [Authorize(Policy = "User")]
        public IActionResult GetRecentPosts(int page = 1)   // query parameter utillizing pagination for performance
        {
            var result = postService.GetRecentPostsAsync(10,page);  // limit is hard coded to 10 for now
            return Ok(result);
        }

        [HttpGet("media/{fileName}")]
        [Authorize(Policy = "User")]
        public IActionResult GetMedia(string fileName)
        {
            var filePath = Path.Combine("Uploads", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, GetContentType(fileName), fileName);
        }

        private static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (provider.TryGetContentType(fileName, out string contentType))
            {
                return contentType;
            }
            return "application/octet-stream";
        }
    }
}