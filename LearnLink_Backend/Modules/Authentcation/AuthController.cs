using LearnLink_Backend.DTOs.StudentDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Modules.Authentcation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthServices _auth) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp(StudentSet student)
        {
            var result = await _auth.SignUp(student);
            if (result == "Success")
                return Ok(result);

            return BadRequest(result);
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel user)
        {
            var result = _auth.Login(user);
            if (result == "invalid")
                return NotFound("could not find user");

            return Ok(result);
        }
        [HttpPatch("changePassword")]
        [Authorize(Policy = "StudentPolicy")]
        [Authorize(Policy = "InstructorPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult ChangePass(string email, string oldPassword, string newPassword)
        {
            return Ok(_auth.ChangePassword(email, oldPassword, newPassword));
        }
    }
}
