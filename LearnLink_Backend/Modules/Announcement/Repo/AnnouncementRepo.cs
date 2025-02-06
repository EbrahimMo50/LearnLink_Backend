using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LearnLink_Backend.Exceptions;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public class AnnouncementRepo(AppDbContext dbContext) : IAnnouncementRepo
    {
        public async Task<AnnouncementModel> CreateAnnouncementAsync(AnnouncementModel announcement)
        {
            var result = await dbContext.Announcements.AddAsync(announcement);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public IEnumerable<AnnouncementModel> GetAllForCourse(int courseId)
        {
            var announcements = dbContext.Announcements.Where(x => x.CourseId == courseId);
            return announcements;
        }

        public void DeleteAnnouncement(int id)
        {
            var element = dbContext.Announcements.FirstOrDefault(x => x.Id == id);
            if(element == null) return;
            dbContext.Announcements.Remove(element);
            dbContext.SaveChanges();
        }

        public async Task<AnnouncementModel?> GetByIdAsync(int id)
        {
            return await dbContext.Announcements.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AnnouncementModel> UpdateAnnouncementAsync(AnnouncementModel announcement)
        {
            var result = dbContext.Announcements.Update(announcement);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}