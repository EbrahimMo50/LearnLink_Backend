using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Hubs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Authentcation.DTOs;
using LearnLink_Backend.Repositories.UserMangementRepo;
using LearnLink_Backend.Services.JWTService;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Text;

namespace LearnLink_Backend.Services.AuthService
{
    public class AuthService(ITokenService tokenService, IUserRepo userRepo, IHubContext<MainHub, IMainHub> mainHub) : IAuthService
    {
        public void SignUp(StudentSet studentVM)
        {
            if (EmailExists(studentVM.Email))
                throw new BadRequestException("Email already exists");

            if (studentVM.Password.Length < 4)
                throw new BadRequestException("Password too short");

            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, studentVM.Password);

            var student = studentVM.ToStudent(HashedPassword, Salt);
            student.CreatedBy = "self";

            userRepo.AddStudent(student);
        }
        //could go with abstraction for admin and instructor to solve redundancy but alot of changes must take place
        public void SignInstructor(Instructor instructor, string password)
        {
            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, password);
            instructor.HashedPassword = HashedPassword;
            instructor.Salt = Salt;
            userRepo.AddInstructor(instructor);
        }
        public void SignAdmin(Admin admin, string password)
        {
            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, password);
            admin.HashedPassword = HashedPassword;
            admin.Salt = Salt;
            userRepo.AddAdmin(admin);
        }
        public LoginSuccessDto Login(LoginDTO user)
        {
            var StudentUser = userRepo.GetStudentByEmail(user.Email);
            if (StudentUser != null)
                if (StudentUser.HashedPassword == Hash(StudentUser.Salt, user.Password))
                    return new LoginSuccessDto() { AccessToken = tokenService.GenerateToken(UniversalUser.ToUser(StudentUser)), User = UniversalUser.ToUser(StudentUser) };


            var InstructorUser = userRepo.GetInstructorByEmail(user.Email);
            if (InstructorUser != null)
                if (InstructorUser.HashedPassword == Hash(InstructorUser.Salt, user.Password))
                    return new LoginSuccessDto() { AccessToken = tokenService.GenerateToken(UniversalUser.ToUser(InstructorUser)), User = UniversalUser.ToUser(InstructorUser) };


            var AdminUser = userRepo.GetAdminByEmail(user.Email);
            if (AdminUser != null)
                if (AdminUser.HashedPassword == Hash(AdminUser.Salt, user.Password))
                    return new LoginSuccessDto() { AccessToken = tokenService.GenerateToken(UniversalUser.ToUser(AdminUser)), User = UniversalUser.ToUser(AdminUser) };

            throw new NotFoundException("User not found");
        }

        public string ChangePassword(string initiatorId, string email, string oldPass, string newPass)
        {
            var student = userRepo.GetStudentById(initiatorId);
            if (student != null)
                if (Hash(student.Salt, oldPass) == student.HashedPassword && student.Email == email)
                {
                    student.HashedPassword = Hash(student.Salt, newPass);
                    student.UpdatedBy = initiatorId;
                    student.UpdateTime = DateTime.UtcNow;
                    mainHub.Clients.Group(initiatorId).LogOut();
                    return "updated pass for student";
                }

            var instructor = userRepo.GetInstructorById(initiatorId);
            if (instructor != null)
                if (Hash(instructor.Salt, oldPass) == instructor.HashedPassword && instructor.Email == email)
                {
                    instructor.HashedPassword = Hash(instructor.Salt, newPass);
                    instructor.UpdatedBy = initiatorId;
                    instructor.UpdateTime = DateTime.UtcNow;
                    mainHub.Clients.Group(initiatorId).LogOut();
                    return "updated pass for instructor";
                }

            var admin = userRepo.GetAdminById(initiatorId);
            if (admin != null)
                if (Hash(admin.Salt, oldPass) == admin.HashedPassword && admin.Email == email)
                {
                    admin.HashedPassword = Hash(admin.Salt, newPass);
                    admin.UpdatedBy = initiatorId;
                    admin.UpdateTime = DateTime.UtcNow;
                    mainHub.Clients.Group(initiatorId).LogOut();
                    return "updated pass for admin";
                }

            throw new NotFoundException("User not found");
        }

        private static string Hash(string salt, string pass)
        {
            var InputBytes = Encoding.UTF8.GetBytes(salt + pass);
            return Convert.ToHexString(SHA256.HashData(InputBytes));

        }
        private static string GenerateSalt()
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Salt = new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return Salt;
        }
        private bool EmailExists(string Email)
        {
            var StudentUserEmail = userRepo.GetStudentByEmail(Email);
            var InstructorUserEmail = userRepo.GetInstructorByEmail(Email);
            var AdminUserEmail = userRepo.GetAdminByEmail(Email);

            if (StudentUserEmail != null || InstructorUserEmail != null || AdminUserEmail != null)
                return true;

            return false;
        }
    }
}