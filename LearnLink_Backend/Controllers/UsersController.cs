using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Repostories.UserRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnLink_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepo _repo) : ControllerBase
    {
        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp(StudentSet student)
        {
            var result = await _repo.SignUp(student);
            if(result == "Success")
                return Ok(result);

            return BadRequest(result);
        }
        [HttpPut("Login")]
        public IActionResult Login(LoginViewModel user)
        {
            var result = _repo.Login(user);
            if (result == "invalid")
                return NotFound("could not find user");

            return Ok(result);
        }
    }
}
