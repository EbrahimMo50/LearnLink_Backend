using LearnLink_Backend.Modules.Adminstration;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Adminsitration
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController(AdministrationService service, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("admins")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddAdmin(AdminSignVM adminAccount)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(service.AddAdmin(adminAccount,issuerId));
        }

        [HttpGet("applications")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetApplications()
        {
            return Ok(await service.GetApplications());
        }

        [HttpGet("students")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetStudents()
        {
            return Ok(service.GetAllStudents());
        }

        [HttpGet("instructors")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetInstructors()
        {
            return Ok(service.GetAllInstructors());
        }

        [HttpPut("application/{id}/accept")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AcceptApplication(int id)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(await service.AcceptApplication(id, issuerId));
        }

        [HttpDelete("user/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(string id)
        {
            return Ok(service.RemoveUser(id));
        }
        [HttpDelete("application/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteApplication(int id)
        {
            service.DeleteApplication(id);
            return Ok();
        }
    }
}