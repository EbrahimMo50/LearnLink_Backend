using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Modules.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Meeting
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController(MeetingService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost()]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> MakeMeeting(MeetingSet meeting)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = await service.Create(meeting, issuerId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult FindById(int id)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = service.FindById(id);
            return Ok(result);
        }

        [HttpGet("instructors")]
        [Authorize(Policy = "InstructorPoilcy")]
        public async Task<IActionResult> GetMeetingsForInstructor()
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(await service.FindMeetingsForInstructor(issuerId));
        }
        [HttpGet("students")]
        [Authorize(Policy = "StudentPoilcy")]
        public async Task<IActionResult> GetMeetingsForStudent()
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(await service.FindMeetingsForStudent(issuerId));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult DeleteMeeting(int id)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            service.Delete(id,issuerId);
            return Ok();
        }
    }
}
