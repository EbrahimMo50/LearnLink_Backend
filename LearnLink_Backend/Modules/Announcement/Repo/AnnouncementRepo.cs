using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Notification;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public class AnnouncementRepo(AppDbContext DbContext, IHttpContextAccessor httpContextAccess) : IAnnouncementRepo
    {
        public async Task<ResponseAPI> CreateAnnouncement(AnnouncementSet announcement)
        {
            if (httpContextAccess.HttpContext == null || httpContextAccess.HttpContext.User.FindFirstValue("Role") == null)
                return new ResponseAPI() { Message = "Error in handling the context of the request"};

            string createrId = httpContextAccess.HttpContext.User.FindFirstValue("Role")!;
            CourseModel? course = await DbContext.Courses.FirstOrDefaultAsync(x => x.Id == announcement.CourseId);

            if (course == null)
                return new ResponseAPI() { Message = "course could not be found" };

            AnnouncementModel obj = new() { Title = announcement.Title, Description = announcement.Description, CourseId = announcement.CourseId, AtDate = DateTime.UtcNow, CreatedBy = createrId};
            return null;
        }

        public void DeleteRepo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseAPI> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResponseAPI>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseAPI> UpdateRepo(int id, AnnouncementSet announcement)
        {
            throw new NotImplementedException();
        }
    }
}
