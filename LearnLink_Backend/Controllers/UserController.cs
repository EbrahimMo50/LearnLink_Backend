using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPut("instructor/{instructorId}/schedule")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult UpdateSchedule(string instructorId, ScheduleUpdate scheduleUpdate)
        {
            scheduleUpdate.InstructorId = instructorId;
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            service.UpdateSchedule(scheduleUpdate, issuerId);
            return NoContent();
        }

        [HttpPatch("student/{studentId}/balance")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddBalance(string studentId, [FromHeader] decimal amount)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = service.AddBalance(studentId, amount, issuerId);
            return Ok(result);
        }

        //two methods to get students and instructors for performance
        [HttpGet("students")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetStudents([FromBody] List<string> ids)
        {
            return Ok(service.GetStudents(ids));
        }

        [HttpGet("instructors")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetInstructors([FromBody] List<string> ids)
        {
            return Ok(service.GetInstructors(ids));
        }
    }
}
