using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.CoursesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> CreateCourse(CourseSet course)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var response = await service.CreateCourseAsync(course, issuerId);
            return CreatedAtRoute(RouteData, response);
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetAllCourses()
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
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var issuerId = HttpContext.User.FindFirstValue("id")!;
            await service.DeleteAsync(id, issuerId);
            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "InstructorPolicy")]
        public async Task<IActionResult> UpdateCourse(int id, CourseSet course)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("error in payload could not find creater");

            var response = await service.UpdateCourseAsync(id, course, issuerId);
            return Ok(response);
        }
        [HttpGet("instructor/{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetCoursesForInstructor(string id)
        {
            return Ok(service.GetCoursesForInstructor(id));
        }

        [HttpPatch("{courseId}/join")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> JoinCourse(int courseId)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("error in payload could not find creater");

            await service.JoinCourseAsync(courseId, issuerId);
            return Ok();
        }
        [HttpPatch("{courseId}/leave")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> LeaveCourse(int courseId)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("error in payload could not find creater");

            await service.LeaveCourseAsync(courseId, issuerId);
            return Ok();
        }
    }
}
