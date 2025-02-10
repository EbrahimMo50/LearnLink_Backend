using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Repositories.SessionsRepo
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
