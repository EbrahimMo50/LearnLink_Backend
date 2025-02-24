using Azure;
using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.SessionsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/course/{courseId}/[controller]")]
    [ApiController]
    public class SessionController(ISessionService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> Create(SessionSet sessionSet, int courseId)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            sessionSet.SetCourseId(courseId);
            if (issuerId == null)
                return BadRequest("could not extract issuer id from http context");

            var response = await service.CreateSessionAsync(sessionSet, issuerId);
            return CreatedAtRoute(RouteData, response);
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
        public async Task<IActionResult> Update(int id, SessionSet sessionSet, int courseId)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            sessionSet.SetCourseId(courseId);
            var result = await service.UpdateAsync(id, sessionSet, issuerId);
            return Ok(result);
        }

        [HttpPatch("{sessionId}/attend")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> AttendSession(int sessionId)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = await service.AttendSessionAsync(sessionId, issuerId);
            return Ok(result);
        }

        [HttpDelete()]
        [Authorize(Policy = "AdminOrInstructor")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return NoContent();
        }
    }
}