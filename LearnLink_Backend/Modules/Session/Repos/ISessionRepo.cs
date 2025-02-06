using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;

namespace LearnLink_Backend.Modules.Session.Repos
{
    public interface ISessionRepo
    {
        public Task<SessionModel> CreateSessionAsync(SessionModel sessionSet);
        public SessionModel? GetById(int id);
        public IEnumerable<SessionModel> GetAll();
        public Task<SessionModel> UpdateAsync(SessionModel session);
        public void Delete(int id);
    }
}
