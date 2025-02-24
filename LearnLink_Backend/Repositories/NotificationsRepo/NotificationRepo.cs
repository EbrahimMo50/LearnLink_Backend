using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.NotificationsRepo
{
    public class NotificationRepo(AppDbContext dbContext) : INotificationRepo
    {
        public async Task<NotificationModel> CreateNotificationAsync(NotificationModel notification)
        {
            var element = await  dbContext.Notifications.AddAsync(notification);
            await dbContext.SaveChangesAsync();
            return element.Entity;
        }

        public void DeleteNotification(int id)
        {
            if (dbContext.Notifications.Where(x => x.Id == id).ExecuteDelete() == 0)
                throw new NotFoundException("could not find notification");
        }

        public NotificationModel? GetNotificationById(int id)
        {
            return dbContext.Notifications
                .Include(x => x.Reciever)
                .FirstOrDefault(x => x.Id == id);
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
