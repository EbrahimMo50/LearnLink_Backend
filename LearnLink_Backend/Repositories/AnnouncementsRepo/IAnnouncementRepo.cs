using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Repositories.AnnouncementsRepo
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
