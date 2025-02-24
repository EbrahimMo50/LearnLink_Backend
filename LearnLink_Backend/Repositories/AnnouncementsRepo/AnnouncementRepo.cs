using Microsoft.EntityFrameworkCore;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;

namespace LearnLink_Backend.Repositories.AnnouncementsRepo
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
            var element = dbContext.Announcements.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException("could not find announcement");
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