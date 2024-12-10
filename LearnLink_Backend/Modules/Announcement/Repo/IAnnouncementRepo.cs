using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public interface IAnnouncementRepo
    {
        public Task<ResponseAPI> CreateAnnouncement(AnnouncementSet announcement);
        public Task<List<ResponseAPI>> GetAll();
        public Task<ResponseAPI> FindById(int id);
        public void DeleteRepo(int id);
        public Task<ResponseAPI> UpdateRepo(int id, AnnouncementSet announcement);
    }
}
