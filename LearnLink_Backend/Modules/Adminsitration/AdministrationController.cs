using LearnLink_Backend.Modules.Adminsitration.Services;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Adminsitration
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController(IAdminstrationService administrationService, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("admins")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddAdmin(AdminSignVM adminAccount)
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
            return Ok();
        }

    }
}