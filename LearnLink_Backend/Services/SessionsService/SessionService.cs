using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Repositories.CoursesRepo;
using LearnLink_Backend.Repositories.SessionsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;

namespace LearnLink_Backend.Services.SessionsService
{
    public class SessionService(ISessionRepo sessionRepo, ICourseRepo courseRepo, IUserRepo userRepo) : ISessionService
    {
        public async Task<SessionModel> CreateSessionAsync(SessionSet sessionSet, string issuerId)
        {
            if (sessionSet.StartTime > sessionSet.EndTime)
                throw new BadRequestException("invalid time entry");

            var course = await courseRepo.GetByIdAsync(sessionSet.CourseId);

            if (course == null || course.Instructor == null)
                throw new NotFoundException("course not found");

            if (course.Instructor.Id.ToString() != issuerId)
                throw new BadRequestException("only instructors of the course can create sessions");

            SessionModel session = new() 
            { 
                CreatedBy = issuerId, 
                CourseId = sessionSet.CourseId,
                StartTime = sessionSet.StartTime,
                EndTime = sessionSet.EndTime,
                Course = course, 
                MeetingLink = sessionSet.MeetingLink 
            };
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

            if (sessionSet.StartTime >= sessionSet.EndTime)
                throw new BadRequestException("invalid time line");

            var course = await courseRepo.GetByIdAsync(sessionSet.CourseId);

            if (course == null || course.Instructor == null)
                throw new NotFoundException("course not found");

            if (course.Instructor.Id.ToString() != issuerId)
                throw new BadRequestException("only instructors of the course can create sessions");

            session.UpdatedBy = issuerId;
            session.UpdateTime = DateTime.Now;
            session.CourseId = course.Id;
            session.Course = course;
            session.MeetingLink = sessionSet.MeetingLink;
            session.StartTime = sessionSet.StartTime;
            session.EndTime = sessionSet.EndTime;

            return await sessionRepo.UpdateAsync(session);
        }
        public void Delete(int id)
        {
            sessionRepo.Delete(id);
        }
        public async Task<string> AttendSessionAsync(int sessionId, string studentId)
        {
            var student = userRepo.GetStudentById(studentId) ?? throw new NotFoundException("student not found");

            var session = sessionRepo.GetById(sessionId) ?? throw new NotFoundException("could not find session");

            if (session.StartTime < DateTime.Now || session.EndTime > DateTime.Now)
                throw new BadRequestException("session is inavtice currently");

            if (session.MeetingLink == null)
                return "Meeting link is to be set later";

            session.AttendendStudent.Add(student);
            await sessionRepo.UpdateAsync(session);

            return session.MeetingLink;
            

            throw new BadRequestException("session is not due today");
        }

        public IEnumerable<string> GetAttendance(int sessionId)
        {
            var session = sessionRepo.GetById(sessionId) ?? throw new NotFoundException("could not find session");
            return session.AttendendStudent.ToList().Select(x => x.Id).Select(x => x.ToString());
        }
    }
}
