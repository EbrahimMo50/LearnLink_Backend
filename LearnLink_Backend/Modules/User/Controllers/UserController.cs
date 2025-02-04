using LearnLink_Backend.Modules.User.DTOs;
using LearnLink_Backend.Modules.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService service,IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("instructors/applications")]
        public IActionResult Apply(InstructorAppSet app)
        {
            var result = service.ApplyForInstructor(app);
            return Ok(result);
        }

        [HttpPut("instructor/{instructorId}/schedule")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult UpdateSchedule(string instructorId, ScheduleSet scheduleSet)
        {
            scheduleSet.SetInstructorId(instructorId);
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(service.UpdateSchedule(scheduleSet,issuerId));
        }

        [HttpPatch("student/{studentId}/balance")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddBalance(string studentId,[FromHeader] decimal amount)
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
