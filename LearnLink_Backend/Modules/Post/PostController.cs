﻿using Azure.Core;
using LearnLink_Backend.Modules.Post.DTOs;
using LearnLink_Backend.Services;
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
        private readonly MediaService _mediaService;
        public PostController(PostService service, IHttpContextAccessor httpContextAccess, MediaService mediaService)
        {
            this._httpContextAccess = httpContextAccess;
            this._service = service;
            this.IssuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            this._mediaService = mediaService;
        }
        [HttpPost()]
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
                imageName = await _mediaService.SaveImage(file);

            post.ImageName = imageName;
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
            return Ok(result);
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