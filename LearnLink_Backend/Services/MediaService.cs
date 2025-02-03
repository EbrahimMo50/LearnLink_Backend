using LearnLink_Backend.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;

namespace LearnLink_Backend.Services
{
    public class MediaService
    {
        public async Task<string> SaveImage(IFormFile file)
        {
            if (file.Length == 0)
                throw new BadRequestException("Empty file uploaded.");

            if (file.ContentType.StartsWith("image/") == false)
                throw new BadRequestException("Invalid file type. Only images are allowed.");

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
            fileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine("Uploads", fileName);

            Directory.CreateDirectory("Uploads");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        [HttpGet("media/{fileName}")]
        public void GetMedia(string fileName)
        {
            var filePath = Path.Combine("Uploads", fileName);

            if (!File.Exists(filePath))
                throw new NotFoundException("file could not be found");

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //return FileStreamResult(fileStream, GetContentType(fileName), fileName);
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
