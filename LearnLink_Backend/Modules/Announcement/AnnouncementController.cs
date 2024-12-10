using LearnLink_Backend.Modules.Announcement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Announcement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController(AnnouncementService service) : ControllerBase
    {

        [HttpGet("")]
        public IActionResult CreateAnnouncement(AnnouncementSet announcement)
        {
            service.CreateAnnouncement(announcement);
            return Ok();
        }
    }
}
