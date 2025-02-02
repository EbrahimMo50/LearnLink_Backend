using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;

namespace LearnLink_Backend.Modules.Session.Repos
{
    public interface ISessionRepo
    {
        public Task<SessionModel> Create(SessionModel sessionSet);
        public SessionModel FindById(int id);
        public IEnumerable<SessionModel> GetAll();
        public Task<SessionModel> Update(int id, SessionSet sessionSet, string issuerId);
        public void Delete(int id);
    }
}
