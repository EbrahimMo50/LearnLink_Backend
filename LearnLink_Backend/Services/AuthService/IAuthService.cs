using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Authentcation.DTOs;

namespace LearnLink_Backend.Services.AuthService
{
    public interface IAuthService
    {
        public void SignUp(StudentSet studentVM);
        public void SignInstructor(Instructor instructor, string password);
        public void SignAdmin(Admin admin, string password);
        public LoginSuccessDto Login(LoginDTO user);
        public string ChangePassword(string initiatorId, string email, string oldPass, string newPass);
        public Task ResetPasswordAsync(string email);
    }
}
