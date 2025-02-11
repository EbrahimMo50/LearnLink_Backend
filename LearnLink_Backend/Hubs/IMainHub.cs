using LearnLink_Backend.DTOs;

namespace LearnLink_Backend.Hubs
{
    // Async suffix is not added to the end because methods are invoked client side and it depends on them
    public interface IMainHub   // the interface represents strongly typed hub, for scaling purposes seperate the hub into multiple interfaces
    {
        // notification releated
        public Task ReceiveNotification(NotificationGet message);

        // auth releated
        public Task LogOut();
    }
}
