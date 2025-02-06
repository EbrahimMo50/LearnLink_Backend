using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Applications;
using LearnLink_Backend.Modules.User.DTOs;
using LearnLink_Backend.Modules.User.Repos.UserMangement;
using LearnLink_Backend.Modules.User.Repos.UserSchedule;

namespace LearnLink_Backend.Modules.User.Services
{
    public class UserService(IUserRepo userRepo, IInstructorScheduleRepo scheduleRepo)
    {
        public InstructorScheduleGet UpdateSchedule(ScheduleSet scheduleSet, string initiatorId)
        {
            var instructor = userRepo.GetInstructorById(initiatorId) ?? throw new NotFoundException("could not find instructor");
            var schedule = scheduleRepo.GetScheduleByInstructorId(instructor.Id.ToString()) ?? throw new NotFoundException("could not find schedule");

            schedule.AvilableDays = scheduleSet.AvilableDays;
            schedule.EndHour = scheduleSet.EndHour;
            schedule.InstructorId = instructor.Id;
            schedule.StartHour = scheduleSet.StartHour;
            schedule.CreatedBy = initiatorId;

            return InstructorScheduleGet.ToDTO(scheduleRepo.UpdateSchedule(schedule));
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

        internal IEnumerable<InstructorGet> GetInstructors(List<string> ids)
        {
            return InstructorGet.ToDTO(userRepo.GetInstructors(ids));
        }
    }
}
