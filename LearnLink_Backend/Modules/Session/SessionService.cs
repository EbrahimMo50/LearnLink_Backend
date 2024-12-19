using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;
using LearnLink_Backend.Modules.Session.Repos;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Session
{
    public class SessionService(AppDbContext DbContext, IHttpContextAccessor httpContextAccess, ISessionRepo repo)
    {
       public async Task<ResponseAPI> Create(SessionSet sessionSet)
        {
            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            if (sessionSet.StartsAt >= sessionSet.EndsAt || sessionSet.Day > DateOnly.FromDateTime(DateTime.Now))
                return new ResponseAPI() { Message = "invalid time line", StatusCode = 400 };

            var course = await DbContext.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == sessionSet.CourseId);

            if (course == null || course.Instructor == null)
                return new ResponseAPI() { Message = "course not found", StatusCode = 404 };

            if (course.Instructor.Id.ToString() != issuerId)
                return new ResponseAPI() { Message = "only instructors of the course can create sessions", StatusCode = 400 };

            SessionModel session = new() { Day = sessionSet.Day, CreatedBy = issuerId, CourseId = sessionSet.CourseId, EndsAt = sessionSet.EndsAt, Course = course, MeetingLink = sessionSet.MeetingLink };
            return await repo.Create(session);
        }
        public ResponseAPI FindById(int id)
        {
            return repo.FindById(id);
        }
        public ResponseAPI GetAll()
        {
            return repo.GetAll();
        }
        public async Task<ResponseAPI> Update(int id, SessionSet sessionSet)
        {
            return await repo.Update(id, sessionSet);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public async Task<ResponseAPI> AttendSession(int sessionId)
        {
            var studentId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var student = await DbContext.Students.FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            if (student == null)
                return new ResponseAPI() { Message = "invalid user", StatusCode = 400 };
            
            var session = await DbContext.Sessions.Include(x => x.AttendendStudent).FirstOrDefaultAsync(x => x.Id == sessionId);
            if (session == null)
                return new ResponseAPI() { Message = "could not find session", StatusCode = 404 };

            if(session.Day == DateOnly.FromDateTime(DateTime.Now)){

                if (session.EndsAt < TimeOnly.FromDateTime(DateTime.Now) || session.StartsAt > TimeOnly.FromDateTime(DateTime.Now))
                    return new ResponseAPI() { Message = "session is inavtice currently", StatusCode = 400 };

                session.AttendendStudent.Add(student);
                await DbContext.SaveChangesAsync();

                return new ResponseAPI() { Message = "student attended", Data = session.MeetingLink };
            }

            return new ResponseAPI() { Message = "session is not due today", StatusCode = 400 };

        }

        public ResponseAPI GetAttendance(int sessionId)
        {
            var session = DbContext.Sessions.Include(x => x.AttendendStudent).FirstOrDefault(x => x.Id == sessionId);

            if(session == null)
                return new ResponseAPI() { Message = "could not find session", StatusCode = 404 };

            return new ResponseAPI() { Data = session.AttendendStudent.ToList() };
        }
    }
}
