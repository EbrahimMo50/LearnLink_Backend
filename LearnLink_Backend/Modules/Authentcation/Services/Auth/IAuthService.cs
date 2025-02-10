using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Authentcation.DTOs;
using LearnLink_Backend.Modules.User.Repos.UserMangement;
using System.Text;

namespace LearnLink_Backend.Modules.Authentcation.Services.Auth
{
    public interface IAuthService
    {
        public void SignUp(StudentSet studentVM);
        public void SignInstructor(Instructor instructor, string password);
        public void SignAdmin(Admin admin, string password);
        public string Login(LoginViewModel user);
        public string ChangePassword(string initiatorId, string email, string oldPass, string newPass);
    }
}
