using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Repositories.NotificationsRepo
{
    public interface INotificationRepo
    {
        public Task<NotificationModel> CreateNotificationAsync(NotificationModel notification);
        public NotificationModel? GetNotificationById(int id);
        public Task<IEnumerable<NotificationModel>> GetNotificationsByInstructorIdAsync(string id);
        public void DeleteNotification(int id);
        public void MarkAsRead(int id);
    }
}
