using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Hubs;
using LearnLink_Backend.Repositories.NotificationsRepo;
using Microsoft.AspNetCore.SignalR;

namespace LearnLink_Backend.Services.NotificationsService
{
    public class NotificationService(INotificationRepo notificationRepo, IHubContext<MainHub, IMainHub> notificationHubContext) : INotificationService
    {
        public async Task SendNotification(NotificationModel notification)
        {
            await notificationHubContext.Clients.All.ReceiveNotification(NotificationGet.ToDTO(notification));
        }
        public NotificationGet GetNotificationById(int id)
        {
            return NotificationGet.ToDTO(notificationRepo.GetNotificationById(id) ?? throw new NotFoundException("notification not found"));
        }

        public async Task<IEnumerable<NotificationModel>> GetNotificationsAsync(string userId)
        {
            return await notificationRepo.GetNotificationsByInstructorIdAsync(userId);
        }

        public void MarkAsRead(int id)
        {
            notificationRepo.MarkAsRead(id);
        }
    }
}
