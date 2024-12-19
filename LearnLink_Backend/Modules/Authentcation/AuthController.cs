using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Modules.Authentcation.DTOs;
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
        [Authorize(Policy = "AdminPolicy, InstructorPolicy")]
        public IActionResult ChangePass(ChangePassDTO passModel)
        {
            return Ok(_auth.ChangePassword(passModel.Email, passModel.OldPassword, passModel.NewPassword));
        }
    }
}
