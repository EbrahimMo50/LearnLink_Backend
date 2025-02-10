using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Repositories.UserMangementRepo;
using LearnLink_Backend.Services.AuthService;

namespace LearnLink_Backend.Services.AdminstrationsService
{
    public class AdministrationService(IAuthService authService, IUserRepo userRepo) : IAdminstrationService
    {
        public void AddAdmin(AdminSignDTO adminAccount, string createrId)
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