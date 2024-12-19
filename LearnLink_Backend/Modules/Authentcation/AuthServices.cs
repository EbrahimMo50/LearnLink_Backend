using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LearnLink_Backend.Modules.Authentcation
{
    public class AuthServices
    {
        private readonly AppDbContext _context;
        private readonly TokenService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthServices(AppDbContext context, TokenService authService, IHttpContextAccessor contexAccess)
        {
            _context = context;
            _authService = authService;
            _httpContextAccessor = contexAccess;

        }
        public async Task<string> SignUp(StudentSet studentVM)
        {
            if (EmailExists(studentVM.Email))
                return "Email already exists";

            if (studentVM.Password.Length < 4)
                return "password too short";

            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, studentVM.Password);

            var student = studentVM.ToStudent(HashedPassword, Salt);
            student.CreatedBy = "self";
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return "Success";
        }
        //could go with abstraction for admin and instructor to solve redundancy buy alot of changes must take place bad design should be avoided in future projects
        public void SignInstructor(Instructor instructor, string password)
        {
            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, password);
            instructor.HashedPassword = HashedPassword;
            instructor.Salt = Salt;
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
        }
        public void SignAdmin(Admin admin, string password)
        {
            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, password);
            admin.HashedPassword = HashedPassword;
            admin.Salt = Salt;
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
        public string Login(LoginViewModel user)
        {
            var StudentUser = _context.Students.FirstOrDefault(x => user.Email == x.Email);
            if (StudentUser != null)
                if (StudentUser.HashedPassword == Hash(StudentUser.Salt, user.Password))
                    return _authService.GenerateToken(UniversalUser.ToUser(StudentUser));


            var InstructorUser = _context.Instructors.FirstOrDefault(x => x.Email == user.Email);
            if (InstructorUser != null)
                if (InstructorUser.HashedPassword == Hash(InstructorUser.Salt, user.Password))
                    return _authService.GenerateToken(UniversalUser.ToUser(InstructorUser));


            var AdminUser = _context.Admins.FirstOrDefault(x => x.Email == user.Email);
            if (AdminUser != null)
                if (AdminUser.HashedPassword == Hash(AdminUser.Salt, user.Password))
                    return _authService.GenerateToken(UniversalUser.ToUser(AdminUser));

            return "invalid";
        }

        public string ChangePassword(string email , string oldPass, string newPass)
        {
            string initiatorId = _httpContextAccessor.HttpContext!.User.FindFirstValue("id")!;

            var student = _context.Students.FirstOrDefault(x => x.Id.ToString() == initiatorId);
            if (student != null)
                if (Hash(student.Salt, oldPass) == student.HashedPassword && student.Email == email)
                {
                    student.HashedPassword = Hash(student.Salt, newPass);
                    student.UpdatedBy = initiatorId;
                    student.UpdateTime = DateTime.UtcNow;
                    return "updated pass for student";
                }

            var instructor = _context.Instructors.FirstOrDefault(x => x.Id.ToString() == initiatorId);
            if (instructor != null)
                if (Hash(instructor.Salt, oldPass) == instructor.HashedPassword && instructor.Email == email)
                {
                    instructor.HashedPassword = Hash(instructor.Salt, newPass);
                    instructor.UpdatedBy = initiatorId;
                    instructor.UpdateTime = DateTime.UtcNow;
                    return "updated pass for instructor";
                }

            var admin = _context.Instructors.FirstOrDefault(x => x.Id.ToString() == initiatorId);
            if (admin != null)
                if (Hash(admin.Salt, oldPass) == admin.HashedPassword && admin.Email == email)
                {
                    admin.HashedPassword = Hash(admin.Salt, newPass);
                    admin.UpdatedBy = initiatorId;
                    admin.UpdateTime = DateTime.UtcNow;
                    return "updated pass for admin";
                }

            return "could not find cerdintals";
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
            var StudentUserEmail = _context.Students.FirstOrDefault(x => Email == x.Email);
            var InstructorUserEmail = _context.Students.FirstOrDefault(x => Email == x.Email);
            var AdminUserEmail = _context.Students.FirstOrDefault(x => Email == x.Email);

            if (StudentUserEmail != null || InstructorUserEmail != null || AdminUserEmail != null)
                return true;

            return false;
        }
    }
}
