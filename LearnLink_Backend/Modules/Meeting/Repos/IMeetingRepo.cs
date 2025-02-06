using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Meeting.Repos
{
    public interface IMeetingRepo
    {
        public Task<MeetingModel> CreateMeetingAsync(MeetingModel meeting);
        public MeetingModel? GetById(int id);
        public Task<IEnumerable<MeetingModel>> GetMeetingsForInstructorAsync(string issuserId);
        public Task<IEnumerable<MeetingModel>> GetMeetingsForStudentAsync(string issuserId);
        public void Delete(int id, string issuserId);
        public IEnumerable<MeetingModel> GetConflictingMeetings(string instructorId, int day);
    }
}
