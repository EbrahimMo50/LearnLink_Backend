using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Post
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IWebHostEnvironment hostingEnvironment) : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("No image file selected.");
                }

                string allowedExtensions = ".jpg;.jpeg;.png;.gif";
                string fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file type. Only " + allowedExtensions + " are allowed.");
                }

                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", Guid.NewGuid().ToString() + fileExtension);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return Ok(new { message = "Image uploaded successfully.", filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetImage(string fileName)
        {
            try
            {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Image not found.");
                }

                byte[] imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                return File(imageBytes, "image/" + Path.GetExtension(fileName).Substring(1));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
