using LearnLink_Backend.Modules.Announcement.DTOs;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public interface IAnnouncementRepo
    {
        public Task<AnnouncementGet> CreateAnnouncement(AnnouncementSet announcement);
        public Task<List<AnnouncementGet>> GetAll();
        public Task<AnnouncementGet> FindById(int id);
        public void DeleteRepo(int id);
        public Task<AnnouncementGet> UpdateRepo(int id, AnnouncementSet announcement);
    }
}
