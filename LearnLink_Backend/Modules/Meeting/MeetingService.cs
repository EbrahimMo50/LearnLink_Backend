using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Modules.Meeting.Repos;
using LearnLink_Backend.Services;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Meeting
{
    public class MeetingService(IMeetingRepo repo, AppDbContext DbContext, IHttpContextAccessor httpContextAccess)
    {
        public Task<ResponseAPI> Create(MeetingSet meeting)
        {
            return repo.Create(meeting);
        }
        public ResponseAPI FindById(int id)
        {
            return repo.FindById(id);
        }
        public Task<ResponseAPI> FindMeetingsForInstructor(string userId)
        {
            return repo.FindMeetingsForInstructor(userId);
        }
        public Task<ResponseAPI> FindMeetingsForStudent(string userId)
        {
            return repo.FindMeetingsForStudent(userId);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
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
        public ResponseAPI UpdateSchedule(string instructorId, ScheduleSet scheduleSet)
        {
            var instructor = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == instructorId);
            string initiatorId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            if (instructor == null)
                return new ResponseAPI() { Message = "could not find instructor", StatusCode = 404 };

            if(instructorId != initiatorId)
                return new ResponseAPI() { Message = "only the instructor can update his schedule", StatusCode = 403 };

            Schedule schedule = new() { 
                AvilableDays = scheduleSet.AvilableDays,
                EndHour = scheduleSet.EndHour,
                InstructorId = instructor.Id,
                StartHour = scheduleSet.StartHour, 
                CreatedBy = instructorId,
            };

            DbContext.Schedules.Add(schedule);
            DbContext.SaveChanges();
            return new ResponseAPI() { Data = schedule };
        }
    }
}
