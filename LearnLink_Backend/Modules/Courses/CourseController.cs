using LearnLink_Backend.Modules.Courses.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(CourseService service) : ControllerBase
    {
        [HttpPost("createCourse")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCourse(CourseSet course)
        {
            var response = await service.CreateCourse(course);
            if (response.StatusCode == 200)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("getAllCourses")]
        public IActionResult GetAllForCourse()
        {
            var response = service.GetAllCourses();
            return Ok(response);
        }
        [HttpGet("getCourse{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var response = await service.FindById(id);
            return response.StatusCode == 200 ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("delete{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteCourse(int id)
        {
            service.Delete(id);
            return Ok();
        }
        [HttpPut("update{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCourse(int id, CourseSet course)
        {
            var response = await service.UpdateCourse(id, course);
            return response.StatusCode == 200 ? Ok(response) : BadRequest(response);
        }
        [HttpPut("joinCourse/{studentId}/{courseId}")] 
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> JoinCourse(String studentId, int courseId)
        {
            var result = await service.JoinCourse(studentId, courseId);
            if(result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("leaveCourse/{studentId}/{courseId}")]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> LeaveCourse(String studentId, int courseId)
        {
            var result = await service.LeaveCourse(studentId, courseId);
            if (result.StatusCode != 200)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
