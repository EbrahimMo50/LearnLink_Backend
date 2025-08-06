using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Services.AdminstrationsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class AdministrationController(IAdminstrationService administrationService, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("admins")]
        public IActionResult AddAdmin(AdminSignDTO adminAccount)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("could not extract issuer id from http context");
            administrationService.AddAdmin(adminAccount, issuerId);
            return Ok();
        }

        [HttpGet("students")]
        public IActionResult GetStudents()
        {
            return Ok(administrationService.GetAllStudents());
        }

        [HttpGet("instructors")]
        public IActionResult GetInstructors()
        {
            return Ok(administrationService.GetAllInstructors());
        }

        [HttpDelete("user/{id}")]
        
        public IActionResult DeleteUser(string id)
        {
            administrationService.RemoveUser(id);
            return NoContent();
        }
        
        [HttpPatch("user/{id}/block")]
        public IActionResult BlockUser(string id)
        {
            administrationService.BlockUser(id);
            return NoContent();
        }

        [HttpPatch("user/{id}/unblock")]
        public IActionResult UnBlockUser(string id)
        {
            administrationService.UnBlockUser(id);
            return NoContent();
        }
    }
}