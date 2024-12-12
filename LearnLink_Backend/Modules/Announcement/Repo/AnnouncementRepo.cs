using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Announcement;
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

            string createrId = httpContextAccess.HttpContext.User.FindFirstValue("id")!;
            CourseModel? course = await DbContext.Courses.FirstOrDefaultAsync(x => x.Id == announcement.CourseId);

            if (course == null)
                return new ResponseAPI() { Message = "course could not be found" , StatusCode = 404 };

            AnnouncementModel obj = new() { Title = announcement.Title, Description = announcement.Description, CourseId = announcement.CourseId, AtDate = DateTime.UtcNow, CreatedBy = createrId, Course = course};
            await DbContext.Announcements.AddAsync(obj);
            await DbContext.SaveChangesAsync();
            return new ResponseAPI() { Message = "Added Succefully", Data = obj };
        }

        public ResponseAPI GetAllForCourse(int courseId)
        {
            var announcements = DbContext.Announcements.Where(x => x.CourseId == courseId);
            return new ResponseAPI() { Data = AnnouncementGet.ToDTO(announcements) };
        }

        public void DeleteAnnouncement(int id)
        {
            var element = DbContext.Announcements.FirstOrDefault(x => x.Id == id);
            if(element == null) return;
            DbContext.Announcements.Remove(element);
            DbContext.SaveChanges();
        }

        public async Task<ResponseAPI> FindById(int id)
        {
            var announcement = await DbContext.Announcements.FirstOrDefaultAsync(x => x.Id == id);
            if(announcement == null)
                return new ResponseAPI() { Message = "could not find the announcement with given id" , StatusCode = 404 };

            return new ResponseAPI() {Data = AnnouncementGet.ToDTO(announcement) };
        }

        public async Task<ResponseAPI> UpdateAnnouncement(int id, AnnouncementUpdate announcement)
        {
            var elementToBeUpdated = await DbContext.Announcements.FirstOrDefaultAsync(x => x.Id == id);
            if(elementToBeUpdated == null)
                return new ResponseAPI() { Message = "could not find the announcement with given id", StatusCode = 404 };
            elementToBeUpdated.Title = announcement.Title;
            elementToBeUpdated.Description = announcement.Description;
            elementToBeUpdated.UpdateTime = DateTime.UtcNow;
            string createrId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            elementToBeUpdated.UpdatedBy = createrId;
            return new ResponseAPI() { Data = elementToBeUpdated };
        }
    }
}