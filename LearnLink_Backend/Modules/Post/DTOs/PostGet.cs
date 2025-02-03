﻿using LearnLink_Backend.Models;

namespace LearnLink_Backend.Modules.Post.DTOs
{
    public class PostGet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string? ImagePath { get; set; } = null;  //nullable because not all posts have images
        public List<string> Likes { get; set; }

        public static PostGet ToDTO(PostModel post)
        {
            return new PostGet { Id = post.Id, Title = post.Title, Description = post.Description, AuthorId = post.Author.Id.ToString(), ImagePath = post.ImagePath, Likes = post.Likes.Select(x => x.Id.ToString()).ToList() };
        }

        public static IEnumerable<PostGet> ToDTO(IEnumerable<PostModel> posts)
        {
            return posts.Select(x => ToDTO(x));
        }
    }
}