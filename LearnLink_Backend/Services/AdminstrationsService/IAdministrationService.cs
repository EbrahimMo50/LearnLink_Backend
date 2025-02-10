using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Adminstration.DTOs;

namespace LearnLink_Backend.Services.AdminstrationsService
{
    public interface IAdminstrationService
    {
        public void AddAdmin(AdminSignDTO adminAccount, string createrId);
        public IEnumerable<StudentGet> GetAllStudents();
        public IEnumerable<InstructorGet> GetAllInstructors();
        public void RemoveUser(string id);
    }
}
