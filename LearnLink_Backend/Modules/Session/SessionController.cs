using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Session
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController(SessionService service) : ControllerBase
    {
        [HttpPost("create")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> Create(SessionSet sessionSet)
        {
            var result = await service.Create(sessionSet);

            if(result.StatusCode != 200)
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

        [HttpGet("getAll")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("attendance")]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult GetAttendance(int sessionId)
        {
            return Ok(service.GetAttendance(sessionId));
        }

        [HttpPut("update/{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> Update(int id, SessionSet sessionSet)
        {
            var result = await service.Update(id,sessionSet);

            if (result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("attendSession/{sessionId}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> AttendSession(int sessionId)
        {
            var result = await service.AttendSession(sessionId);
            if (result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("delete")]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok();
        }
    }
}