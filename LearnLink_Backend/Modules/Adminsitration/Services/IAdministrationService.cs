using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Modules.Adminstration.DTOs;

namespace LearnLink_Backend.Modules.Adminsitration.Services
{
    public interface IAdminstrationService
    {
        public void AddAdmin(AdminSignVM adminAccount, string createrId);
        public IEnumerable<StudentGet> GetAllStudents();
        public IEnumerable<InstructorGet> GetAllInstructors();
        public void RemoveUser(string id);
    }
}
