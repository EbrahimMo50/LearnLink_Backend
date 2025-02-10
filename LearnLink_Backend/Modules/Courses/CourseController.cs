using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCourse(CourseSet course)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var response = await service.CreateCourseAsync(course, issuerId);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetAllForCourse()
        {
            var response = service.GetAllCourses();
            return Ok(response);
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> FindById(int id)
        {
            var response = await service.GetByIdAsync(id);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteCourse(int id)
        {
            service.Delete(id);
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCourse(int id, CourseSet course)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("error in payload could not find creater");

            var response = await service.UpdateCourseAsync(id, course, issuerId);
            return Ok(response);
        }
        [HttpPost("{courseId}/students")] 
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> JoinCourse(int courseId)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("error in payload could not find creater");

            await service.JoinCourseAsync(courseId, issuerId);
            return Ok();
        }
        [HttpDelete("{courseId}/students/{studentId}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> LeaveCourse(String studentId, int courseId)
        {
            await service.LeaveCourseAsync(studentId, courseId);
            return Ok();
        }
    }
}
