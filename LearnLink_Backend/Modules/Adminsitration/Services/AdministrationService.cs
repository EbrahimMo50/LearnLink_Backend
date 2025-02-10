using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Modules.Authentcation.Services.Auth;
using LearnLink_Backend.Modules.User.Repos.UserMangement;

namespace LearnLink_Backend.Modules.Adminsitration.Services
{
    public class AdministrationService(IAuthService authService, IUserRepo userRepo) : IAdminstrationService
    {
        public void AddAdmin(AdminSignVM adminAccount, string createrId)
        {
            Admin admin = new() { Name = adminAccount.Name, Email = adminAccount.Email, CreatedBy = createrId };
            authService.SignAdmin(admin, adminAccount.Password);
        }

        public IEnumerable<StudentGet> GetAllStudents()
        {
            return StudentGet.ToDTO(userRepo.GetStudents(null));
        }

        public IEnumerable<InstructorGet> GetAllInstructors()
        {
            return InstructorGet.ToDTO(userRepo.GetInstructors(null));
        }

        // will not allow admins to delete each other
        public void RemoveUser(string id)
        {
            var student = userRepo.GetStudentById(id);
            if (student != null)
            {
                userRepo.DeleteStudent(id);
            }

            var instructor = userRepo.GetInstructorById(id);
            if (instructor != null)
            {
                userRepo.DeleteInstructor(id);
            }
        }
    }
}