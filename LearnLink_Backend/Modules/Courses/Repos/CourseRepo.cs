﻿using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Courses.Repos
{
    public class CourseRepo(AppDbContext DbContext) : ICourseRepo
    {
        public async Task<CourseModel> CreateCourseAsync(CourseModel course)
        {
            var result = await DbContext.Courses.AddAsync(course);
            await DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            return [.. DbContext.Courses.Include(x => x.Instructor)];
        }

        public async Task<CourseModel?> GetByIdAsync(int id)
        {
            var course = await DbContext.Courses.Include(x => x.Instructor)
                .Include(x => x.Students)
                .Include(x => x.Instructor)
                .Include(x => x.Announcements)
                .Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == id);
            return course;
        }

        public void Delete(int id)
        {
            var element = DbContext.Courses.FirstOrDefault(x => x.Id == id);
            if(element == null)
                return;
            DbContext.Courses.Remove(element);
            DbContext.SaveChanges();
        }

        public async Task<CourseModel> UpdateCourseAsync(CourseModel course)
        {
            var result = DbContext.Courses.Update(course);
            await DbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
