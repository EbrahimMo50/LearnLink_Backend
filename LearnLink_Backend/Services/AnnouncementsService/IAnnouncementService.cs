using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.AnnouncementsService
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
