using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace LearnLink_Backend.Hubs
{
    // hub for users to manage connections and disconnections
    public class MainHub(ConcurrentDictionary<string, List<string>> connectedUsers) : Hub<IMainHub>
    {
        public override Task OnConnectedAsync()
        {
            try
            {
                connectedUsers[Context.GetHttpContext()!.User.FindFirstValue("id")!].Add(Context.ConnectionId);
                Console.WriteLine($"user connected with connection id = {Context.ConnectionId}");
                Console.WriteLine($"Id = {Context.GetHttpContext()!.User.FindFirstValue("id")!}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                string userId = Context.GetHttpContext()!.User.FindFirstValue("id")!;

                if (connectedUsers.TryGetValue(userId, out List<string> connectionIds))
                {
                    connectionIds.Remove(Context.ConnectionId);

                    if (connectionIds.Count != 0)
                        connectionIds.Remove(userId);
                    else
                        Console.WriteLine($"User {userId} not found in connected users.");
                }
                Console.WriteLine($"user disconnected with connection id = {Context.ConnectionId}");
                Console.WriteLine($"Id = {Context.GetHttpContext()!.User.FindFirstValue("id")!}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            foreach (var item in connectedUsers)
                item.Value.ForEach(x => Console.WriteLine(x));

            return base.OnDisconnectedAsync(exception);
        }
    }
}