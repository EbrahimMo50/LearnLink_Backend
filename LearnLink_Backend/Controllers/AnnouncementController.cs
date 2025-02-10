using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.AnnouncementsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/course/{courseId}/[controller]/")]
    [ApiController]
    public class AnnouncementController(IAnnouncementService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {

        [HttpPost]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> CreateAnnouncement(AnnouncementSet announcement, int courseId)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var response = await service.CreateAnnouncementAsync(announcement, courseId, issuerId);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetAllForCourse(int courseId)
        {
            var response = service.GetAllForCourse(courseId);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> FindById(int id)
        {
            var response = await service.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult DeleteAnnouncement(int id)
        {
            service.DeleteAnnouncement(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> UpdateAnnouncement(int id, AnnouncementUpdate announcement)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var response = await service.UpdateAnnouncementAsync(id, announcement, issuerId);
            return Ok(response);
        }
    }
}
