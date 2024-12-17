using LearnLink_Backend.Modules.Meeting.DTOs;
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
        [Authorize(Policy = "StudentPolicy")]
        [Authorize(Policy = "InstructorPoilcy")]
        [Authorize(Policy = "AdminPoilcy")]
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
        [Authorize(Policy = "StudentPolicy")]
        [Authorize(Policy = "InstructorPoilcy")]
        [Authorize(Policy = "AdminPoilcy")]
        public IActionResult DeleteMeeting(int id)
        {
            service.Delete(id);
            return Ok();
        }

        [HttpPut("updateBalance/{studentId}/{amount}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddBalance(string studentId, decimal amount)
        {
            var result = service.AddBalance(studentId, amount);
            if(result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("updateSchedule")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult UpdateSchedule(ScheduleSet scheduleSet)
        {
            return Ok(service.UpdateSchedule(scheduleSet));
        }
    }
}
