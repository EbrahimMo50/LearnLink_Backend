using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Session.DTOs;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Session.Repos
{
    public class SessionRepo(AppDbContext DbContext) : ISessionRepo
    {
        public async Task<SessionModel> Create(SessionModel session)
        {
            await DbContext.Sessions.AddAsync(session);
            await DbContext.SaveChangesAsync();
            return session;
        }

        public void Delete(int id)
        {
            var session = DbContext.Sessions.FirstOrDefault(x => x.Id == id);
            if(session != null)
                DbContext.Sessions.Remove(session);
            DbContext.SaveChanges();
        }

        public SessionModel FindById(int id)
        {
            var session = DbContext.Sessions.Include(x => x.AttendendStudent).FirstOrDefault(x => x.Id == id);
            if (session != null)
                return session;
            throw new NotFoundException("could not find session");
        }

        public IEnumerable<SessionModel> GetAll()
        { 
            return [.. DbContext.Sessions.Include(x => x.AttendendStudent)];
        }

        public async Task<SessionModel> Update(int id, SessionSet sessionSet, string issuerId)
        {
            var session = await DbContext.Sessions.FirstOrDefaultAsync(x => x.Id == id);

            if (session == null)
                throw new NotFoundException("course not found");

            if (sessionSet.StartsAt >= sessionSet.EndsAt || sessionSet.Day > DateOnly.FromDateTime(DateTime.Now))
               throw new BadRequestException("invalid time line");

            var course = await DbContext.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == sessionSet.CourseId);

            if (course == null || course.Instructor == null)
                throw new NotFoundException("course not found");

            if (course.Instructor.Id.ToString() != issuerId)
                throw new BadRequestException("only instructors of the course can create sessions");

            session.UpdatedBy = issuerId;
            session.UpdateTime = DateTime.Now;
            session.CourseId = course.Id;
            session.Course = course;
            session.MeetingLink = sessionSet.MeetingLink;
            session.StartsAt = sessionSet.StartsAt;
            session.EndsAt = sessionSet.EndsAt;
            session.Day = sessionSet.Day;
    
            await DbContext.SaveChangesAsync();
            return session;
        }
    }
}
