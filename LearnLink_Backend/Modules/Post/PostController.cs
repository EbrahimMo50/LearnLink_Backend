using Azure.Core;
using LearnLink_Backend.Modules.Post.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Post
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private string IssuerId;
        private readonly PostService _service;
        private readonly IHttpContextAccessor _httpContextAccess;
        PostController(PostService service, IHttpContextAccessor httpContextAccess)
        {
            this._httpContextAccess = httpContextAccess;
            this._service = service;
            this.IssuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
        }
        [HttpPost()]
        public IActionResult CreatePost(PostSet post)
        {
            var result = _service.CreatePost(post, IssuerId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var result = _service.GetPost(id);
            return Ok(result);
        }
        [HttpGet("recent")]
        public IActionResult GetRecentPosts(int page = 1)   // query parameter utillizing pagination for performance
        {
            var result = _service.GetRecentPosts(10,page);  // limit is hard coded to 10 for now
            return Ok();
        }
    }
}


//[HttpPost("upload")]
//public async Task<IActionResult> UploadImage()
//{
//    if (Request.Form.Files.Count == 0)
//    {
//        return BadRequest("No file uploaded.");
//    }

//    var file = Request.Form.Files[0]; // this will take the first file to be found not multiple files

//    if (file.Length == 0)
//    {
//        return BadRequest("Empty file uploaded.");
//    }

//    if (file.ContentType.StartsWith("image/") == false)
//    {
//        return BadRequest("Invalid file type. Only images are allowed.");
//    }

//    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
//    fileName = $"{Guid.NewGuid()}_{fileName}";
//    var filePath = Path.Combine("Uploads", fileName);

//    Directory.CreateDirectory("Uploads");

//    using (var stream = new FileStream(filePath, FileMode.Create))
//    {
//        await file.CopyToAsync(stream);
//    }

//    return Ok(new { FileName = fileName, FilePath = filePath });
//}

//[HttpGet("media/{fileName}")]
//public IActionResult GetMedia(string fileName)
//{
//    var filePath = Path.Combine("Uploads", fileName);

//    if (!System.IO.File.Exists(filePath))
//    {
//        return NotFound();
//    }

//    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
//    return File(fileStream, GetContentType(fileName), fileName);
//}

//private static string GetContentType(string fileName)
//{
//    var provider = new FileExtensionContentTypeProvider();

//    if (provider.TryGetContentType(fileName, out string contentType))
//    {
//        return contentType;
//    }
//    return "application/octet-stream";
//}