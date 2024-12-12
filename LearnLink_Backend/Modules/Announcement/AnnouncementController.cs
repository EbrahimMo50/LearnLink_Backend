using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Announcement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Announcement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController(AnnouncementService service) : ControllerBase
    {

        [HttpPost("createAnnouncement")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> CreateAnnouncement(AnnouncementSet announcement)
        {
            var response = await service.CreateAnnouncement(announcement);
            if(response.StatusCode == 200)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("getAllForCourse{courseId}")]
        public IActionResult GetAllForCourse(int courseId)
        {
            var response = service.GetAllForCourse(courseId);
            return Ok(response);
        }
        [HttpGet("getAnnouncement{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var response = await service.FindById(id);
            return response.StatusCode == 200 ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("delete{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult DeleteAnnouncement(int id)
        {
            service.DeleteAnnouncement(id);
            return Ok();
        }
        [HttpPut("update{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> UpdateAnnouncement(int id, AnnouncementUpdate announcement)
        {
            var response = await service.UpdateAnnouncement(id, announcement);
            return response.StatusCode == 200 ? Ok(response) : BadRequest(response);
        }
    }
}
