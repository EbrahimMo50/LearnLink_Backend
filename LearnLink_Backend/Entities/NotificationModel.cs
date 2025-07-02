using LearnLink_Backend.Models;

namespace LearnLink_Backend.Entities
{
    // notifications will be system driven only, admins can NOT create notifications
    public class NotificationModel
    {
        public int Id { get; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Instructor Reciever { get; set; } = null!;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
