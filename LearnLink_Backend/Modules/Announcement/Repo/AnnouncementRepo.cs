using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Services;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public class AnnouncementRepo(AppDbContext DbContext, IHttpContextAccessor httpContextAccess) : IAnnouncementRepo
    {
        public Task<AnnouncementGet> CreateAnnouncement(AnnouncementSet announcement)
        {
            // Console.WriteLine(httpContextAccess.HttpContext.User.FindFirstValue("Role"));

            return null;
        }

        public void DeleteRepo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AnnouncementGet> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnnouncementGet>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AnnouncementGet> UpdateRepo(int id, AnnouncementSet announcement)
        {
            throw new NotImplementedException();
        }
    }
}
