using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Session.Repos
{
    public class SessionRepo(AppDbContext DbContext, IHttpContextAccessor httpContextAccess) : ISessionRepo
    {
        public async Task<ResponseAPI> Create(SessionModel session)
        {
            await DbContext.Sessions.AddAsync(session);
            return new ResponseAPI() { Data = session };
        }

        public void Delete(int id)
        {
            var session = DbContext.Sessions.FirstOrDefault(x => x.Id == id);
            if(session != null)
                DbContext.Sessions.Remove(session);
            DbContext.SaveChanges();
        }

        public ResponseAPI FindById(int id)
        {
            var session = DbContext.Sessions.FirstOrDefault(x => x.Id == id);
            if (session != null)
                return new ResponseAPI() { Data = session };
            return new ResponseAPI() { Message = "could not find session" , StatusCode = 404};
        }

        public ResponseAPI GetAll()
        {
            return new ResponseAPI() { Data = DbContext.Sessions.ToList() };
        }

        public async Task<ResponseAPI> Update(int id, SessionSet sessionSet)
        {
            var session = await DbContext.Sessions.FirstOrDefaultAsync(x => x.Id == id);

            var issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;

            if (session == null)
                return new ResponseAPI() { Message = "course not found", StatusCode = 404 };

            if (sessionSet.StartsAt >= sessionSet.EndsAt || sessionSet.Day > DateOnly.FromDateTime(DateTime.Now))
                return new ResponseAPI() { Message = "invalid time line", StatusCode = 400 };

            var course = await DbContext.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == sessionSet.CourseId);

            if (course == null || course.Instructor == null)
                return new ResponseAPI() { Message = "course not found", StatusCode = 404 };

            if (course.Instructor.Id.ToString() != issuerId)
                return new ResponseAPI() { Message = "only instructors of the course can create sessions", StatusCode = 400 };

            session.UpdatedBy = issuerId;
            session.UpdateTime = DateTime.Now;

 
            session.CourseId = course.Id;
            session.Course = course;

            session.MeetingLink = sessionSet.MeetingLink;
            session.StartsAt = sessionSet.StartsAt;
            sessionSet.EndsAt = sessionSet.EndsAt;
            session.Day = sessionSet.Day;

            await DbContext.SaveChangesAsync();
            return new ResponseAPI() { Data = session };
        }
    }
}
