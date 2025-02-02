using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Announcement
{
    public class AnnouncementService(IAnnouncementRepo repo, AppDbContext DbContext)
    {
        public async Task<AnnouncementModel> CreateAnnouncement(AnnouncementSet announcement, int courseId, string createrId)
        {
            CourseModel? course = await DbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
                throw new NotFoundException("course could not be found");

            AnnouncementModel obj = new() { Title = announcement.Title, Description = announcement.Description, CourseId = courseId, AtDate = DateTime.UtcNow, CreatedBy = createrId, Course = course };
            return await repo.CreateAnnouncement(obj);
        }

        public IEnumerable<AnnouncementGet> GetAllForCourse(int courseId)
        {
            return AnnouncementGet.ToDTO(repo.GetAllForCourse(courseId));
        }
        public async Task<AnnouncementGet> FindById(int id)
        {
            return  AnnouncementGet.ToDTO(await repo.FindById(id));
        }
        public void DeleteAnnouncement(int id)
        {
            repo.DeleteAnnouncement(id);
        }
        public async Task<AnnouncementModel> UpdateAnnouncement(int id, AnnouncementUpdate announcement, string createrId)
        {
            return await repo.UpdateAnnouncement(id, announcement, createrId);
        }
    }
}
