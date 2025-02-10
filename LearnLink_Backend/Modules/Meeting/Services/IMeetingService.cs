using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Modules.Meeting.Repos;
using LearnLink_Backend.Modules.User.Repos.UserMangement;

namespace LearnLink_Backend.Modules.Meeting.Services
{
    public interface IMeetingService
    {
        public Task<MeetingModel> CreateMeetingAsync(MeetingSet meeting, string createrId);
        public MeetingGet GetById(int id);
        public Task<IEnumerable<MeetingGet>> GetMeetingsForInstructorAsync(string issuerId);
        public Task<IEnumerable<MeetingGet>> GetMeetingsForStudentAsync(string issuerId);
        public void Delete(int id, string issuerId);
    }
}
