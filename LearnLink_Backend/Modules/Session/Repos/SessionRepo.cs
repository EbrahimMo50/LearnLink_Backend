using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Session.Repos
{
    public class SessionRepo(AppDbContext DbContext) : ISessionRepo
    {
        public async Task<SessionModel> CreateSessionAsync(SessionModel session)
        {
            var result = await DbContext.Sessions.AddAsync(session);
            await DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public void Delete(int id)
        {
            var session = DbContext.Sessions.FirstOrDefault(x => x.Id == id);
            if(session != null)
                DbContext.Sessions.Remove(session);
            DbContext.SaveChanges();
        }

        public SessionModel? GetById(int id)
        {
            return DbContext.Sessions
                .Include(x => x.AttendendStudent)
                .Include(x => x.Course)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<SessionModel> GetAll()
        { 
            return [.. DbContext.Sessions.Include(x => x.AttendendStudent)];
        }

        public async Task<SessionModel> UpdateAsync(SessionModel session)
        {
            var result = DbContext.Sessions.Update(session);
            await DbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
