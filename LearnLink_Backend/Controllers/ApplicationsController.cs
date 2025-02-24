using Azure;
using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.ApplicationsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController(IHttpContextAccessor httpContextAccess, IApplicationService applicationService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Apply(ApplicationSet app)
        {
            var result = applicationService.ApplyForInstructor(app);
            return CreatedAtRoute(RouteData, result); ;
        }


        [HttpPut("{id}/accept")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AcceptApplication(int id)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("could not extract issuer from http context");

            applicationService.AcceptApplication(id, issuerId);
            // might add email notification using email service in future here
            return Ok("signed instructor");
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetApplications()
        {
            return Ok(applicationService.GetApplications());
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteApplication(int id)
        {
            applicationService.DeleteApplication(id);
            return NoContent();
        }
    }
}
