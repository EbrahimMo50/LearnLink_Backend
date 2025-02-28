﻿using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Repositories.PostsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;

namespace LearnLink_Backend.Services.PostsService
{
    public class PostService(IPostRepo postRepo, IUserRepo userRepo) : IPostService
    {
        public async Task<PostModel> CreatePostAsync(PostSet postSet, string IssuerId)
        {
            PostModel post = new();
            Admin admin = userRepo.GetAdminById(IssuerId) ?? throw new NotFoundException("Admin not found");
            post.Author = admin;
            post.Title = postSet.Title;
            post.Description = postSet.Description;
            post.CreatedBy = IssuerId;
            post.ImagePath = postSet.ImageName;
            return await postRepo.CreatePostAsync(post);
        }

        public PostGet GetPost(int id)
        {
            return PostGet.ToDTO(postRepo.GetPostById(id) ?? throw new NotFoundException("could not find post"));
        }

        public async Task<IEnumerable<PostGet>> GetRecentPostsAsync(int limit, int page)
        {
            return PostGet.ToDTO(await postRepo.GetRecentPostsAsync(limit, page));
        }

        public void DeletePost(int id)
        {
            postRepo.DeletePost(id);
        }

        public PostModel UpdatePost(int id, PostSet newPost, string IssuerId)
        {
            if (userRepo.GetAdminById(IssuerId) == null)
                throw new NotFoundException("Admin not found");

            var post = postRepo.GetPostById(id) ?? throw new NotFoundException("Post not found");

            post.Description = newPost.Description;
            post.Title = newPost.Title;
            post.ImagePath = newPost.ImageName;
            post.UpdatedBy = IssuerId;
            post.UpdateTime = DateTime.UtcNow;
            return postRepo.UpdatePost(post);
        }
    }
}
