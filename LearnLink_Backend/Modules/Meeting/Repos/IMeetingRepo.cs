using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Meeting.DTOs;

namespace LearnLink_Backend.Modules.Meeting.Repos
{
    public interface IMeetingRepo
    {
        public Task<ResponseAPI> Create(MeetingSet meeting);
        public ResponseAPI FindById(int id);
        public Task<ResponseAPI> FindMeetingsForInstructor(string userId);
        public Task<ResponseAPI> FindMeetingsForStudent(string userId);
        public void Delete(int id);
    }
}
