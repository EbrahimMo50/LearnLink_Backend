using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.NotificationsService
{
    public interface INotificationService
    {
        public Task SendNotification(NotificationModel notification);
        public void MarkAsRead(int id);
        public Task<IEnumerable<NotificationModel>> GetNotificationsAsync(string userId);
        public NotificationGet GetNotificationById(int id);
    }
}
