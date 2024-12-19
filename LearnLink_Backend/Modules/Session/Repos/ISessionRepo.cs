using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Session.DTOs;

namespace LearnLink_Backend.Modules.Session.Repos
{
    public interface ISessionRepo
    {
        public Task<ResponseAPI> Create(SessionModel sessionSet);
        public ResponseAPI FindById(int id);
        public ResponseAPI GetAll();
        public Task<ResponseAPI> Update(int id, SessionSet sessionSet);
        public void Delete(int id);
    }
}
