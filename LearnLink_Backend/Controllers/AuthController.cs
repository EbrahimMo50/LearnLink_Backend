using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Authentcation.DTOs;
using LearnLink_Backend.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("sign-up")]
        public IActionResult SignUp(StudentSet student)
        {
            authService.SignUp(student);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO user)
        {
            var result = authService.Login(user);   // the token is returned here
            return Ok(result);
        }

        [HttpPatch("change-password")]
        [Authorize(Policy = "User")]
        public IActionResult ChangePass(ChangePassDTO passModel)
        {
            string? issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (issuerId == null)
                return BadRequest("could not extract issuer id");
            return Ok(authService.ChangePassword(issuerId, passModel.Email, passModel.OldPassword, passModel.NewPassword));
        }
        [HttpPut("reset-password")]
        public IActionResult ResetPassword([FromBody] string email)
        {
            authService.ResetPasswordAsync(email);
            return NoContent();
        }
    }
}
