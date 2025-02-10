using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.SessionsService
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
