using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.NotificationsService
{
    public interface INotificationService
    {
        public Task SendNotification(NotificationModel notification);
        public void MarkAsRead(string issuerId, int id);
        public Task<IEnumerable<NotificationModel>> GetNotificationsByInstructorIdAsync(string issuerId, string userId);
        public NotificationGet GetNotificationById(string userId, int id);
        public void DeleteNotification(string issuerId, int id);
    }
}
