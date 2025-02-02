using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LearnLink_Backend.Modules.Post
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController() : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage()
        {
            if (Request.Form.Files.Count == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var file = Request.Form.Files[0]; // this will take the first file to be found not multiple files

            if (file.Length == 0)
            {
                return BadRequest("Empty file uploaded.");
            }

            if (file.ContentType.StartsWith("image/") == false)
            {
                return BadRequest("Invalid file type. Only images are allowed.");
            }

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
            fileName = $"{Guid.NewGuid()}_{fileName}"; 
            var filePath = Path.Combine("Uploads", fileName); 

            Directory.CreateDirectory("Uploads");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FileName = fileName, FilePath = filePath });
        }
    }
}
