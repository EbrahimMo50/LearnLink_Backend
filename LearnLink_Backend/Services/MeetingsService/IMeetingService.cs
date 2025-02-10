using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.MeetingsService
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
