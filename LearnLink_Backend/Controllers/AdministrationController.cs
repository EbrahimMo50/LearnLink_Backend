using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Services.AdminstrationsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController(IAdminstrationService administrationService, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("admins")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddAdmin(AdminSignDTO adminAccount)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("could not extract issuer id from http context");
            administrationService.AddAdmin(adminAccount, issuerId);
            return Ok();
        }

        [HttpGet("students")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetStudents()
        {
            return Ok(administrationService.GetAllStudents());
        }

        [HttpGet("instructors")]
        [Authorize(Policy = "User")]
        public IActionResult GetInstructors()
        {
            return Ok(administrationService.GetAllInstructors());
        }

        [HttpDelete("user/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(string id)
        {
            administrationService.RemoveUser(id);
            return NoContent();
        }
        
        [HttpPatch("user/{id}/block")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult BlockUser(string id)
        {
            administrationService.BlockUser(id);
            return NoContent();
        }

        [HttpPatch("user/{id}/unblock")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult UnBlockUser(string id)
        {
            administrationService.UnBlockUser(id);
            return NoContent();
        }
    }
}