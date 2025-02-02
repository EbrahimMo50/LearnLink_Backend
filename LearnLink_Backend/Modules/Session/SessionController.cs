using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Session
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController(SessionService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost()]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> Create(SessionSet sessionSet)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = await service.Create(sessionSet,issuerId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public IActionResult FindById(int id)
        {
            var result = service.FindById(id);
            return Ok(result);
        }

        [HttpGet()]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{sessionId}/attendance")]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult GetAttendance(int sessionId)
        {
            return Ok(service.GetAttendance(sessionId));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> Update(int id, SessionSet sessionSet)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = await service.Update(id,sessionSet,issuerId);
            return Ok(result);
        }

        [HttpPatch("{sessionId}/attend")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> AttendSession(int sessionId)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = await service.AttendSession(sessionId,issuerId);
            return Ok(result);
        }

        [HttpDelete()]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok();
        }
    }
}