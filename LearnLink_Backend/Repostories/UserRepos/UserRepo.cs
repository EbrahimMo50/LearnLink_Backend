using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Services;
using System.Security.Cryptography;
using System.Text;

namespace LearnLink_Backend.Repostories.UserRepos
{
    public class UserRepo : IUserRepo
    {
        private AppDbContext _context;
        private Authentaction _authService;
        public UserRepo(AppDbContext context, Authentaction authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<string> SignUp(StudentSet studentVM)
        {
            if (EmailExists(studentVM.Email))
                return "Email already exists";

            if (studentVM.Password.Length < 4)
                return "password too short";

            var Salt = GenerateSalt();
            var HashedPassword = Hash(Salt, studentVM.Password);

            await _context.Students.AddAsync(studentVM.ToStudent(HashedPassword, Salt));
            await _context.SaveChangesAsync();

            return "Success";
        }
        public string Login(LoginViewModel user)
        {
            var StudentUser = _context.Students.FirstOrDefault(x => user.Email == x.Email);
            if (StudentUser != null)            
                if (StudentUser.HashedPassword == Hash(StudentUser.Salt, user.Password))
                    return _authService.GenerateToken(UniversalUser.ToUser(StudentUser));

     
            var InstructorUser = _context.Instructors.FirstOrDefault(x=> x.Email == user.Email);
            if (InstructorUser != null)
                if (InstructorUser.HashedPassword == Hash(InstructorUser.Salt, user.Password))
                    return _authService.GenerateToken(UniversalUser.ToUser(InstructorUser));

            
            var AdminUser = _context.Admins.FirstOrDefault(x=> x.Email == user.Email);
            if (AdminUser != null)
                if (AdminUser.HashedPassword == Hash(AdminUser.Salt, user.Password))
                    return _authService.GenerateToken(UniversalUser.ToUser(AdminUser));

            return "invalid";
        }
        public string ApplyForInstructor(InstructorAppDto instructorApp)
        {
            throw new NotImplementedException("shutup");
            //if (EmailExists(instructorApp.Email))
            //    return "Email Exists";

            //_context.InstructorApplications.Add(instructorApp);
            //_context.SaveChanges();
        }

        public void ChangePassword(LoginViewModel user, string newPass)
        {
            throw new NotImplementedException();
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
