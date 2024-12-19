using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Modules.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Meeting
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController(MeetingService service) : ControllerBase
    {
        [HttpPost("makeMeeting")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> MakeMeeting(MeetingSet meeting)
        {
            var result = await service.Create(meeting);
            if (result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("findById/{id}")]
        [Authorize(Policy = "User")]
        public IActionResult FindById(int id)
        {
            var result = service.FindById(id);
            if (result.StatusCode != 200) 
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("instructorMeetings")]
        [Authorize(Policy = "InstructorPoilcy")]
        public async Task<IActionResult> GetMeetingsForInstructor()
        {
            return Ok(await service.FindMeetingsForInstructor());
        }
        [HttpGet("studentMeetings")]
        [Authorize(Policy = "StudentPoilcy")]
        public async Task<IActionResult> GetMeetingsForStudent()
        {
            return Ok(await service.FindMeetingsForStudent());
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "User")]
        public IActionResult DeleteMeeting(int id)
        {
            service.Delete(id);
            return Ok();
        }
    }
}
