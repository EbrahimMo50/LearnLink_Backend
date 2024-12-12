using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;

namespace LearnLink_Backend.Modules.Announcement.Repo
{
    public interface IAnnouncementRepo
    {
        public Task<ResponseAPI> CreateAnnouncement(AnnouncementSet announcement);
        public ResponseAPI GetAllForCourse(int courseId);
        public Task<ResponseAPI> FindById(int id);
        public void DeleteAnnouncement(int id);
        public Task<ResponseAPI> UpdateAnnouncement(int id, AnnouncementUpdate announcement);
    }
}
