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
        [Authorize(Policy = "AdminPolicy")]
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

    }
}