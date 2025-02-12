using LearnLink_Backend.Services.NotificationsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationService notificaitonService) : ControllerBase
    {
        [HttpPatch("{id}/mark-read")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult MarkAsRead(int id)
        {
            var issuerId = HttpContext.User.FindFirst("id")!.Value;
            notificaitonService.MarkAsRead(issuerId, id);
            return Ok();
        }
        [HttpGet("instructor/{userId}")]
        [Authorize(Policy = "AdminOrInstructor")]
        public async Task<IActionResult> GetNotificationsByInstructor(string userId)
        {
            var issuerId = HttpContext.User.FindFirst("id")!.Value;
            return Ok(await notificaitonService.GetNotificationsByInstructorIdAsync(issuerId, userId));
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult GetNotificationById(int id)
        {
            var issuerId = HttpContext.User.FindFirst("id")!.Value;
            return Ok(notificaitonService.GetNotificationById(issuerId, id));
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult DeleteNotification(int id)
        {
            var issuerId = HttpContext.User.FindFirst("id")!.Value;
            notificaitonService.DeleteNotification(issuerId, id);
            return Ok();
        }
    }
}
