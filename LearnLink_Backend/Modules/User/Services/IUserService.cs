using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Modules.User.DTOs;

namespace LearnLink_Backend.Modules.User.Services
{
    public interface IUserService
    {
        public InstructorScheduleGet UpdateSchedule(ScheduleSet scheduleSet, string initiatorId);
        public StudentGet AddBalance(string id, decimal balance, string updaterId);
        public IEnumerable<StudentGet> GetStudents(List<string> ids);
        public IEnumerable<InstructorGet> GetInstructors(List<string> ids);
    }
}
