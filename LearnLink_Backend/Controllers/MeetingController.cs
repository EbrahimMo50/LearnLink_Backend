using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.MeetingsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController(IMeetingService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> MakeMeeting(MeetingSet meeting)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("could not extract issuer id from http context");

            var result = await service.CreateMeetingAsync(meeting, issuerId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult FindById(int id)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("could not extract issuer id from http context");

            var result = service.GetById(id);
            return Ok(result);
        }

        [HttpGet("instructors")]
        [Authorize(Policy = "InstructorPoilcy")]
        public async Task<IActionResult> GetMeetingsForInstructor()
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(await service.GetMeetingsForInstructorAsync(issuerId));
        }
        [HttpGet("students")]
        [Authorize(Policy = "StudentPoilcy")]
        public async Task<IActionResult> GetMeetingsForStudent()
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(await service.GetMeetingsForStudentAsync(issuerId));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult DeleteMeeting(int id)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            service.Delete(id, issuerId);
            return Ok();
        }
    }
}
