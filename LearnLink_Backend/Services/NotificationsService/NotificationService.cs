using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Hubs;
using LearnLink_Backend.Repositories.NotificationsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;
using Microsoft.AspNetCore.SignalR;

namespace LearnLink_Backend.Services.NotificationsService
{
    public class NotificationService(
        INotificationRepo notificationRepo,
        IUserRepo userRepo,
        IHubContext<MainHub, IMainHub> notificationHubContext) : INotificationService
    {
        public async Task SendNotification(NotificationModel notification)
        {
             await notificationRepo.CreateNotificationAsync(notification);

             await
                 notificationHubContext.Clients
                .Group(notification.Reciever.Id.ToString())
                .ReceiveNotification(NotificationGet.ToDTO(notification));
        }
        public NotificationGet GetNotificationById(string issuerId, int id)
        {
            var user = userRepo.GetAdminById(issuerId);
            var notification = notificationRepo.GetNotificationById(id) ?? throw new NotFoundException("notification not found");

            if (user != null || notification.Reciever.Id.ToString() == issuerId)
                return NotificationGet.ToDTO(notification);

            throw new BadRequestException("user is unathuorized");
        }

        public async Task<IEnumerable<NotificationModel>> GetNotificationsByInstructorIdAsync(string issuerId, string userId)
        {
            var user = userRepo.GetAdminById(issuerId);

            if(user != null || userId == issuerId)
                return await notificationRepo.GetNotificationsByInstructorIdAsync(userId);

            throw new BadRequestException("user not found");
        }

        public void MarkAsRead(string issuerId, int id)
        {
            var notification = notificationRepo.GetNotificationById(id) ?? throw new NotFoundException("notification not found");
            if (notification.Reciever.Id.ToString() == issuerId)
                notificationRepo.MarkAsRead(id);
        }

        public void DeleteNotification(string issuerId, int id)
        {
            var user = userRepo.GetAdminById(issuerId);
            var notification = notificationRepo.GetNotificationById(id) ?? throw new NotFoundException("notification not found");

            if (user != null || notification.Reciever.Id.ToString() == issuerId)
                notificationRepo.DeleteNotification(id);
        }
    }
}
