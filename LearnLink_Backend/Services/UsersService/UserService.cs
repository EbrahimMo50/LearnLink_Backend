using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Repositories.UserMangementRepo;

namespace LearnLink_Backend.Services.UsersService
{
    public class UserService(IUserRepo userRepo) : IUserService
    {
        public IEnumerable<DayAvailability> UpdateSchedule(ScheduleUpdate scheduleUpdate, string initiatorId)
        {
            var instructor = userRepo.GetInstructorById(initiatorId) ?? throw new NotFoundException("could not find instructor");
            instructor.Schedule = scheduleUpdate.NewSchedule;

            return userRepo.UpdateInstructor(instructor).Schedule;
        }
        public StudentGet AddBalance(string id, decimal balance, string updaterId)
        {
            var student = userRepo.GetStudentById(id) ?? throw new NotFoundException("could not find user");
            student.UpdatedBy = updaterId;
            student.UpdateTime = DateTime.UtcNow;
            student.Balance += balance;
            return StudentGet.ToDTO(userRepo.UpdateStudent(student));
        }

        public IEnumerable<StudentGet> GetStudents(List<string> ids)
        {
            return StudentGet.ToDTO(userRepo.GetStudents(ids));
        }

        public IEnumerable<InstructorGet> GetInstructors(List<string> ids)
        {
            return InstructorGet.ToDTO(userRepo.GetInstructors(ids));
        }
    }
}
