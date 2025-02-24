using LearnLink_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.MeetingsRepo
{
    public interface IMeetingRepo
    {
        public Task<MeetingModel> CreateMeetingAsync(MeetingModel meeting);
        public MeetingModel? GetById(int id);
        public Task<IEnumerable<MeetingModel>> GetMeetingsForInstructorAsync(string issuserId);
        public Task<IEnumerable<MeetingModel>> GetMeetingsForStudentAsync(string issuserId);
        public void Delete(int id);
        public IEnumerable<MeetingModel> GetConflictingMeetings(string instructorId, int day);
    }
}
