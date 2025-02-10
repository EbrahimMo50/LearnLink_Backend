using LearnLink_Backend.DTOs;

namespace LearnLink_Backend.Services.UsersService
{
    public interface IUserService
    {
        public InstructorScheduleGet UpdateSchedule(ScheduleSet scheduleSet, string initiatorId);
        public StudentGet AddBalance(string id, decimal balance, string updaterId);
        public IEnumerable<StudentGet> GetStudents(List<string> ids);
        public IEnumerable<InstructorGet> GetInstructors(List<string> ids);
    }
}
