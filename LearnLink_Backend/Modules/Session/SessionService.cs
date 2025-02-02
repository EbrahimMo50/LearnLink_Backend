using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Session.DTOs;
using LearnLink_Backend.Modules.Session.Repos;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Session
{
    public class SessionService(AppDbContext DbContext, ISessionRepo repo)
    {
       public async Task<SessionModel> Create(SessionSet sessionSet, string issuerId)
        {
            if (sessionSet.StartsAt >= sessionSet.EndsAt || sessionSet.Day > DateOnly.FromDateTime(DateTime.Now))
                throw new BadRequestException("invalid time line");

            var course = await DbContext.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == sessionSet.CourseId);

            if (course == null || course.Instructor == null)
                throw new NotFoundException("course not found");

            if (course.Instructor.Id.ToString() != issuerId)
                throw new BadRequestException("only instructors of the course can create sessions");

            SessionModel session = new() { Day = sessionSet.Day, CreatedBy = issuerId, CourseId = sessionSet.CourseId, EndsAt = sessionSet.EndsAt, Course = course, MeetingLink = sessionSet.MeetingLink };
            return await repo.Create(session);
        }
        public SessionGet FindById(int id)
        {
            return SessionGet.ToDTO(repo.FindById(id));
        }
        public IEnumerable<SessionGet> GetAll()
        {
            return SessionGet.ToDTO(repo.GetAll());
        }
        public async Task<SessionModel> Update(int id, SessionSet sessionSet, string issuerId)
        {
            return await repo.Update(id, sessionSet, issuerId);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public async Task<string> AttendSession(int sessionId, string studentId)
        {
            var student = await DbContext.Students.FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            if (student == null)
                throw new BadRequestException("invalid user");
            
            var session = await DbContext.Sessions.Include(x => x.AttendendStudent).FirstOrDefaultAsync(x => x.Id == sessionId);
            if (session == null)
                throw new NotFoundException("could not find session");

            if(session.Day == DateOnly.FromDateTime(DateTime.Now)){

                if (session.EndsAt < TimeOnly.FromDateTime(DateTime.Now) || session.StartsAt > TimeOnly.FromDateTime(DateTime.Now))
                   throw new BadRequestException("session is inavtice currently");

                session.AttendendStudent.Add(student);
                await DbContext.SaveChangesAsync();

                return session.MeetingLink;
            }

            throw new BadRequestException("session is not due today");
        }

        public IEnumerable<string> GetAttendance(int sessionId)
        {
            var session = DbContext.Sessions.Include(x => x.AttendendStudent).FirstOrDefault(x => x.Id == sessionId);

            if(session == null)
               throw new NotFoundException("could not find session");

            return session.AttendendStudent.ToList().Select(x => x.Id).Select(x => x.ToString());
        }
    }
}
