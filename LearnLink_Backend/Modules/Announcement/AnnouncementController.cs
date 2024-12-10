using LearnLink_Backend.Modules.Announcement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Announcement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController(AnnouncementService service) : ControllerBase
    {

        [HttpPost("createAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement(AnnouncementSet announcement)
        {
            var response = await service.CreateAnnouncement(announcement);
            if(response.StatusCode == 200)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
