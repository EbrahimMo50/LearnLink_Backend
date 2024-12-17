using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Modules.Meeting.Repos;
using LearnLink_Backend.Modules.User.DTOs;
using LearnLink_Backend.Services;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Meeting
{
    public class MeetingService(IMeetingRepo repo, AppDbContext DbContext, IHttpContextAccessor httpContextAccess)
    {
        public Task<ResponseAPI> Create(MeetingSet meeting)
        {
            return repo.Create(meeting);
        }
        public ResponseAPI FindById(int id)
        {
            return repo.FindById(id);
        }
        public Task<ResponseAPI> FindMeetingsForInstructor()
        {
            return repo.FindMeetingsForInstructor();
        }
        public Task<ResponseAPI> FindMeetingsForStudent()
        {
            return repo.FindMeetingsForStudent();
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
