
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.DTOs
{   
    public class NotificationGet
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public string RecieverId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false;

        public static NotificationGet ToDTO(NotificationModel notification)
        {
            return new NotificationGet
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                RecieverId = notification.Reciever.Id.ToString()!,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }

        public static IEnumerable<NotificationGet> ToDTO(IEnumerable<NotificationModel> notifications)
        {
            return notifications.Select(x => ToDTO(x)).ToList();
        }
    }
}
