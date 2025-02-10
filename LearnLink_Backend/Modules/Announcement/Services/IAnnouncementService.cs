using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Courses.Repos;

namespace LearnLink_Backend.Modules.Announcement.Services
{
    public interface IAnnouncementService
    {
        public Task<AnnouncementModel> CreateAnnouncementAsync(AnnouncementSet announcement, int courseId, string createrId);
        public IEnumerable<AnnouncementGet> GetAllForCourse(int courseId);
        public Task<AnnouncementGet> GetByIdAsync(int id);
        public void DeleteAnnouncement(int id);
        public Task<AnnouncementModel> UpdateAnnouncementAsync(int id, AnnouncementUpdate announcement, string createrId);
    }
}
