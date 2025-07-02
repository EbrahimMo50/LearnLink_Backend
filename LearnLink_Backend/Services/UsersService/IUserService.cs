using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;

namespace LearnLink_Backend.Services.UsersService
{
    public interface IUserService
    {
        public IEnumerable<DayAvailability> UpdateSchedule(ScheduleUpdate scheduleSet, string initiatorId);
        public StudentGet AddBalance(string id, decimal balance, string updaterId);
        public IEnumerable<StudentGet> GetStudents(List<string> ids);
        public IEnumerable<InstructorGet> GetInstructors(List<string> ids);
    }
}
