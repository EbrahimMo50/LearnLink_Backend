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
        [HttpPost("applyForInstructor")]
        public IActionResult Apply(InstructorAppSet app)
        {
            var result = service.ApplyForInstructor(app);
            return Ok(result);
        }

        [HttpPut("updateSchedule")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult UpdateSchedule(ScheduleSet scheduleSet)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(service.UpdateSchedule(scheduleSet,issuerId));
        }

        [HttpPut("updateBalance/{studentId}/{amount}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddBalance(string studentId, decimal amount)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = service.AddBalance(studentId, amount, issuerId);
            return Ok(result);
        }
    }
}
