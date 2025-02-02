using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Modules.Authentcation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Authentcation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthServices _auth, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(StudentSet student)
        {
            var result = await _auth.SignUp(student);
            if (result == "Success")
                return Ok(result);

            return BadRequest(result);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel user)
        {
            var result = _auth.Login(user);
            if (result == "invalid")
                return NotFound("could not find user");

            return Ok(result);
        }
        [HttpPatch("change-password")]
        [Authorize(Policy = "User")]
        public IActionResult ChangePass(ChangePassDTO passModel)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            return Ok(_auth.ChangePassword(issuerId, passModel.Email, passModel.OldPassword, passModel.NewPassword));
        }
    }
}
