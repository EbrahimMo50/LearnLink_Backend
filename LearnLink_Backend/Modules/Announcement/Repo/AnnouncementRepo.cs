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
    public class AnnouncementRepo(AppDbContext DbContext) : IAnnouncementRepo
    {
        public async Task<AnnouncementModel> CreateAnnouncement(AnnouncementModel announcement)
        {
            await DbContext.Announcements.AddAsync(announcement);
            await DbContext.SaveChangesAsync();
            return announcement;
        }

        public IEnumerable<AnnouncementModel> GetAllForCourse(int courseId)
        {
            var announcements = DbContext.Announcements.Where(x => x.CourseId == courseId);
            return announcements;
        }

        public void DeleteAnnouncement(int id)
        {
            var element = DbContext.Announcements.FirstOrDefault(x => x.Id == id);
            if(element == null) return;
            DbContext.Announcements.Remove(element);
            DbContext.SaveChanges();
        }

        public async Task<AnnouncementModel> FindById(int id)
        {
            var announcement = await DbContext.Announcements.FirstOrDefaultAsync(x => x.Id == id);
            return announcement == null ? throw new NotFoundException("could not find the announcement with given id") : announcement;
        }

        public async Task<AnnouncementModel> UpdateAnnouncement(int id, AnnouncementUpdate announcement, string createrId)
        {
            var elementToBeUpdated = await DbContext.Announcements.FirstOrDefaultAsync(x => x.Id == id);
            if(elementToBeUpdated == null)
                throw new NotFoundException("could not find the announcement with given id");

            elementToBeUpdated.Title = announcement.Title;
            elementToBeUpdated.Description = announcement.Description;
            elementToBeUpdated.UpdateTime = DateTime.UtcNow;
            elementToBeUpdated.UpdatedBy = createrId;
            await DbContext.SaveChangesAsync();
            return elementToBeUpdated;
        }
    }
}