using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public interface IAnnouncementRepo
    {
        public Task<AnnouncementModel> CreateAnnouncementAsync(AnnouncementModel announcement);
        public IEnumerable<AnnouncementModel> GetAllForCourse(int courseId);
        public Task<AnnouncementModel?> GetByIdAsync(int id);
        public void DeleteAnnouncement(int id);
        public Task<AnnouncementModel> UpdateAnnouncementAsync(AnnouncementModel announcement);
    }
}
