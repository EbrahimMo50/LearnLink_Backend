using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace LearnLink_Backend.Hubs
{
    // hub for users to manage connections and disconnections
    [Authorize("User")]
    public class MainHub : Hub<IMainHub>
    {
        public async override Task OnConnectedAsync()
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.User.FindFirstValue("id")!);


            //try
            //{
            //    if(!connectedUsers.TryAdd(Context.GetHttpContext()!.User.FindFirstValue("id")!, [Context.ConnectionId]))
            //    {
            //        connectedUsers[Context.GetHttpContext()!.User.FindFirstValue("id")!].Add(Context.ConnectionId);
            //    }

            //    //Console.WriteLine($"user connected with connection id = {Context.ConnectionId}");
            //    //Console.WriteLine($"Id = {Context.GetHttpContext()!.User.FindFirstValue("id")!} \n\n");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.User.FindFirstValue("id")!);

            //try
            //{
            //    string userId = Context.GetHttpContext()!.User.FindFirstValue("id")!;

            //    if (connectedUsers.TryGetValue(userId, out List<string> connectionIds))
            //    {
            //        if (connectionIds.Count == 1)
            //            connectedUsers.TryRemove(userId, out _);

            //        else 
            //            connectionIds.Remove(Context.ConnectionId);

            //    }
            //    //Console.WriteLine($"user disconnected with connection id = {Context.ConnectionId}");
            //    //Console.WriteLine($"Id = {Context.GetHttpContext()!.User.FindFirstValue("id")!}\n\n");

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            await base.OnDisconnectedAsync(exception);
        }
    }
}