using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Services;
using LearnLink_Backend.Services.PostsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostService postService, IHttpContextAccessor httpContextAccess, MediaService mediaService) : ControllerBase
    {
        [HttpPost()]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreatePost([FromForm]PostSet post)
        {
            
            if (post.Image != null)
            {
                var imageName = await mediaService.SaveImage(post.Image);
                post.ImageName = imageName;
            }

            string? IssuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");

            if (IssuerId == null)
                return BadRequest("could not extract issuer id from http context");

            post.AuthorId = IssuerId;
            var response = await postService.CreatePostAsync(post, IssuerId);
            return CreatedAtRoute(RouteData, response);
        }
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var result = postService.GetPost(id);
            return Ok(result);
        }
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentPosts(int limit = 10, int page = 1)   // query parameter utillizing pagination for performance
        {
            var result = await postService.GetRecentPostsAsync(limit, page);
            return Ok(result);
        }
        [HttpPatch("{id}/react")]
        public IActionResult ReactToPost(int id)
        {
            var user = HttpContext.User.FindFirstValue("id")!;
            return Ok(postService.ReactToPost(id, user));
        }

        [HttpGet("media/{fileName}")]
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

        [HttpPost("{postId}/comment")]
        [Authorize(Policy = "StudentPolicy")]
        public IActionResult AddComment(CommentSet commentDto, int postId)
        {
            commentDto.PostId = postId;
            var Issuer = HttpContext.User.FindFirst("id") ?? throw new BadRequestException("cant extract user");
            commentDto.UserGuid = Issuer.Value;
            var comment = postService.AddComment(commentDto);
            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.Id}, comment);
        }

        [HttpGet("{postId}/check-like")]
        [Authorize(Policy = "StudentPolicy")]
        public IActionResult CheckLike(int postId)
        {
            var issuerId = HttpContext.User.FindFirst("id") ?? throw new BadRequestException("cant extract user");
            return Ok(postService.CheckLike(postId, issuerId.Value));
        }

        [HttpGet("{postId}/comment")]
        [Authorize("User")]
        public IActionResult GetAllComments(int postId)
        {
            return Ok(postService.GetAllComments(postId));
        }

        [HttpGet("comment/{commentId}")]
        [Authorize("User")]
        public IActionResult GetCommentById(int commentId)
        {
            return Ok(postService.GetComment(commentId));
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