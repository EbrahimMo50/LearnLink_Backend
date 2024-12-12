using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Announcement.Repo;

namespace LearnLink_Backend.Modules.Announcement
{
    public class AnnouncementService(IAnnouncementRepo repo)
    {
        public Task<ResponseAPI> CreateAnnouncement(AnnouncementSet announcement)
        {
            return repo.CreateAnnouncement(announcement);
        }
        public ResponseAPI GetAllForCourse(int courseId)
        {
            return repo.GetAllForCourse(courseId);
        }
        public Task<ResponseAPI> FindById(int id)
        {
            return repo.FindById(id);
        }
        public void DeleteAnnouncement(int id)
        {
            repo.DeleteAnnouncement(id);
        }
        public Task<ResponseAPI> UpdateAnnouncement(int id, AnnouncementUpdate announcement)
        {
            return repo.UpdateAnnouncement(id, announcement);
        }
    }
}
