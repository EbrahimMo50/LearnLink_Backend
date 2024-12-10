using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Announcement.Repo;

namespace LearnLink_Backend.Modules.Announcement
{
    public class AnnouncementService(IAnnouncementRepo repo)
    {
        public Task<AnnouncementGet> CreateAnnouncement(AnnouncementSet announcement)
        {
            return repo.CreateAnnouncement(announcement);
        }
    }
}
