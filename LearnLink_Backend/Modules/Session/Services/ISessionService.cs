using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Courses.Repos;
using LearnLink_Backend.Modules.Session.DTOs;
using LearnLink_Backend.Modules.Session.Repos;
using LearnLink_Backend.Modules.User.Repos.UserMangement;

namespace LearnLink_Backend.Modules.Session.Services
{
    public interface ISessionService
    {
        public Task<SessionModel> CreateSessionAsync(SessionSet sessionSet, string issuerId);
        public SessionGet FindById(int id);
        public IEnumerable<SessionGet> GetAll();
        public Task<SessionModel> UpdateAsync(int id, SessionSet sessionSet, string issuerId);
        public void Delete(int id);
        public Task<string> AttendSessionAsync(int sessionId, string studentId);
        public IEnumerable<string> GetAttendance(int sessionId);
    }
}
