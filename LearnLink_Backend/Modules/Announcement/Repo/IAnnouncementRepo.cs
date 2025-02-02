using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public interface IAnnouncementRepo
    {
        public Task<AnnouncementModel> CreateAnnouncement(AnnouncementModel announcement);
        public IEnumerable<AnnouncementModel> GetAllForCourse(int courseId);
        public Task<AnnouncementModel> FindById(int id);
        public void DeleteAnnouncement(int id);
        public Task<AnnouncementModel> UpdateAnnouncement(int id, AnnouncementUpdate announcement, string createrId);
    }
}
