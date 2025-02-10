using Microsoft.AspNetCore.SignalR;

namespace LearnLink_Backend.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.Users("someone").SendAsync("ReceiveMessage", user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
