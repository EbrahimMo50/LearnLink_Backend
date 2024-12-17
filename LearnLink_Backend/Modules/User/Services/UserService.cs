using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.Models;
using LearnLink_Backend.Modules.User.DTOs;
using LearnLink_Backend.Services;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.User.Services
{
    public class UserService(IHttpContextAccessor httpContextAccess,AppDbContext DbContext)
    {
        public ResponseAPI UpdateSchedule(ScheduleSet scheduleSet)
        {
            string initiatorId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var instructor = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == initiatorId);

            if (instructor == null)
                return new ResponseAPI() { Message = "could not find instructor", StatusCode = 404 };

            Schedule schedule = new()
            {
                AvilableDays = scheduleSet.AvilableDays,
                EndHour = scheduleSet.EndHour,
                InstructorId = instructor.Id,
                StartHour = scheduleSet.StartHour,
                CreatedBy = initiatorId,
            };

            DbContext.Schedules.Add(schedule);
            DbContext.SaveChanges();
            return new ResponseAPI() { Data = schedule };
        }
        public ResponseAPI AddBalance(string id, decimal balance)
        {
            var student = DbContext.Students.FirstOrDefault(x => x.Id.ToString() == id);
            string updaterId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            if (student == null)
                return new ResponseAPI() { Message = "could not find user", StatusCode = 404 };

            student.UpdatedBy = updaterId;
            student.UpdateTime = DateTime.UtcNow;
            student.Balance += balance;
            DbContext.SaveChanges();
            return new ResponseAPI() { Message = "updated user balance", Data = student };
        }
        public ResponseAPI ApplyForInstructor(InstructorAppSet applicationSet)
        {
            if (DbContext.InstructorApplications.Any())
                return new ResponseAPI() { Message = "application already exists", StatusCode = 400 };
            
            InstructorApplicationModel application = new()
            {
                Name = applicationSet.Name,
                Email = applicationSet.Email,
                Password = applicationSet.Password,
                Messsage = applicationSet.Messsage,
                Nationality = applicationSet.Nationality,
                SpokenLanguage = applicationSet.SpokenLanguage,
                CreatedBy = "self"
            };
            DbContext.InstructorApplications.Add(application);
            DbContext.SaveChanges();
            return new ResponseAPI() { Message = "succefully applied" };
        }
    }
}
