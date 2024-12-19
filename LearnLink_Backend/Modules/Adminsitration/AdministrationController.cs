using LearnLink_Backend.Modules.Adminstration;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Adminsitration
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController(AdministrationService service) : ControllerBase
    {
        [HttpPost("addAdmin")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AddAdmin(AdminSignVM adminAccount)
        {
            return Ok(service.AddAdmin(adminAccount));
        }

        [HttpGet("getApplications")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetApplications()
        {
            return Ok(await service.GetApplications());
        }

        [HttpGet("getStudents")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetStudents()
        {
            return Ok(service.GetAllStudents());
        }

        [HttpGet("getInstructors")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetInstructors()
        {
            return Ok(service.GetAllInstructors());
        }

        [HttpPut("acceptApplication/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AcceptApplication(int id)
        {
            return Ok(await service.AcceptApplication(id));
        }

        [HttpDelete("deleteUser/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(string id)
        {
            return Ok(service.RemoveUser(id));
        }
        [HttpDelete("deleteApplication/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteApplication(int id)
        {
            service.DeleteApplication(id);
            return Ok();
        }
    }
}