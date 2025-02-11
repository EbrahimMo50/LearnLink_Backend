using LearnLink_Backend.Services.NotificationsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationService notificaitonService) : ControllerBase
    {
        // to be removed
        [HttpPost]
        public IActionResult SendNotificationTest()
        {
            //remove set from instructor id
            notificaitonService.SendNotification(new Entities.NotificationModel() { Title = "Hello world", Message = "hello from back", Reciever = new Models.Instructor() { Id = new Guid() } });
            return Ok();
        }
    }
}
