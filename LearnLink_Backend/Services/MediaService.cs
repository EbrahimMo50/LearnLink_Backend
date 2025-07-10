using LearnLink_Backend.Exceptions;
using System.Net.Http.Headers;

namespace LearnLink_Backend.Services
{
    public class MediaService
    {
        public Stream? GetMedia(string mediaId)
        {
            var filePath = Path.Combine("Uploads", mediaId);

            if (!File.Exists(filePath))
                return null;

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }

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
    }
}
