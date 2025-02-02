using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Meeting.DTOs;

namespace LearnLink_Backend.Modules.Meeting.Repos
{
    public interface IMeetingRepo
    {
        public Task<MeetingModel> Create(MeetingModel meeting);
        public MeetingModel FindById(int id);
        public Task<IEnumerable<MeetingModel>> FindMeetingsForInstructor(string issuserId);
        public Task<IEnumerable<MeetingModel>> FindMeetingsForStudent(string issuserId);
        public void Delete(int id, string issuserId);
    }
}
