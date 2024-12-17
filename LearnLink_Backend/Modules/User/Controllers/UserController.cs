using LearnLink_Backend.Modules.User.DTOs;
using LearnLink_Backend.Modules.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService service) : ControllerBase
    {
        [HttpPost("applyForInstructor")]
        public IActionResult Apply(InstructorAppSet app)
        {
            var result = service.ApplyForInstructor(app);
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

        [HttpPut("updateBalance/{studentId}/{amount}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddBalance(string studentId, decimal amount)
        {
            var result = service.AddBalance(studentId, amount);
            if (result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
