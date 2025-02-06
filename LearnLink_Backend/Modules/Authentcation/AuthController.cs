using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Modules.Authentcation.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Authentcation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthServices authService, IHttpContextAccessor httpContextAccess) : ControllerBase
    {
        [HttpPost("sign-up")]
        public IActionResult SignUp(StudentSet student)
        {
            authService.SignUp(student);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel user)
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
    }
}
