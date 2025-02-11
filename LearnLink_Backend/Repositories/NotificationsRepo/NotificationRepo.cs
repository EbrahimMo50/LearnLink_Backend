using LearnLink_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.NotificationsRepo
{
    public class NotificationRepo(AppDbContext dbContext) : INotificationRepo
    {
        public void DeleteNotification(int id)
        {
            dbContext.Notifications.Where(x => x.Id == id).ExecuteDelete();
        }

        public NotificationModel? GetNotificationById(int id)
        {
            return dbContext.Notifications.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<NotificationModel>> GetNotificationsByInstructorIdAsync(string id)
        {
            return await dbContext.Notifications.Where(x => x.Reciever.Id.ToString() == id).ToListAsync();
        }

        public void MarkAsRead(int id)
        {
            var notification = dbContext.Notifications.FirstOrDefault(x => x.Id == id);
            if(notification == null)
                return;
            notification.IsRead = true;
            dbContext.SaveChanges();
        }
    }
}
