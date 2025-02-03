using LearnLink_Backend.Modules.Courses.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(CourseService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost()]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCourse(CourseSet course)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var response = await service.CreateCourse(course, issuerId);
            return Ok(response);
        }
        [HttpGet()]
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
            var response = await service.FindById(id);
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
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var response = await service.UpdateCourse(id, course, issuerId);
            return Ok(response);
        }
        [HttpPost("{courseId}/students")] 
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> JoinCourse(int courseId)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var result = await service.JoinCourse(courseId, issuerId);
            return Ok(result);
        }
        [HttpDelete("{courseId}/students/{studentId}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> LeaveCourse(String studentId, int courseId)
        {
            var result = await service.LeaveCourse(studentId, courseId);
            return Ok(result);
        }
    }
}
