using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Courses.Repos;
using LearnLink_Backend.Modules.Session.DTOs;
using LearnLink_Backend.Modules.Session.Repos;
using LearnLink_Backend.Modules.User.Repos.UserMangement;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Session
{
    public class SessionService(ISessionRepo sessionRepo, ICourseRepo courseRepo, IUserRepo userRepo)
    {
       public async Task<SessionModel> CreateSessionAsync(SessionSet sessionSet, string issuerId)
        {
            if (sessionSet.StartsAt >= sessionSet.EndsAt || sessionSet.Day > DateOnly.FromDateTime(DateTime.Now))
                throw new BadRequestException("invalid time line");

            var course = await courseRepo.GetByIdAsync(sessionSet.GetCourseId());

            if (course == null || course.Instructor == null)
                throw new NotFoundException("course not found");

            if (course.Instructor.Id.ToString() != issuerId)
                throw new BadRequestException("only instructors of the course can create sessions");

            SessionModel session = new() { Day = sessionSet.Day, CreatedBy = issuerId, CourseId = sessionSet.GetCourseId(), EndsAt = sessionSet.EndsAt, Course = course, MeetingLink = sessionSet.MeetingLink };
            return await sessionRepo.CreateSessionAsync(session);
        }
        public SessionGet FindById(int id)
        {
            return SessionGet.ToDTO(sessionRepo.GetById(id) ?? throw new NotFoundException("could not find session"));
        }
        public IEnumerable<SessionGet> GetAll()
        {
            return SessionGet.ToDTO(sessionRepo.GetAll());
        }
        public async Task<SessionModel> UpdateAsync(int id, SessionSet sessionSet, string issuerId)
        {
            var session = sessionRepo.GetById(id);

            if (session == null)
                throw new NotFoundException("course not found");

            if (sessionSet.StartsAt >= sessionSet.EndsAt || sessionSet.Day > DateOnly.FromDateTime(DateTime.Now))
                throw new BadRequestException("invalid time line");

            var course = await courseRepo.GetByIdAsync(sessionSet.GetCourseId());

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

            return await sessionRepo.UpdateAsync(session);
        }
        public void Delete(int id)
        {
            sessionRepo.Delete(id);
        }
        public async Task<string> AttendSessionAsync(int sessionId, string studentId)
        {
            var student = userRepo.GetStudentById(studentId) ?? throw new NotFoundException("student not found");
            
            var session = sessionRepo.GetById(sessionId);
            if (session == null)
                throw new NotFoundException("could not find session");

            if(session.Day == DateOnly.FromDateTime(DateTime.Now)){

                if (session.EndsAt < TimeOnly.FromDateTime(DateTime.Now) || session.StartsAt > TimeOnly.FromDateTime(DateTime.Now))
                   throw new BadRequestException("session is inavtice currently");

                session.AttendendStudent.Add(student);
                await sessionRepo.UpdateAsync(session);

                return session.MeetingLink;
            }

            throw new BadRequestException("session is not due today");
        }

        public IEnumerable<string> GetAttendance(int sessionId)
        {
            var session = sessionRepo.GetById(sessionId) ?? throw new NotFoundException("could not find session");
            return session.AttendendStudent.ToList().Select(x => x.Id).Select(x => x.ToString());
        }
    }
}
