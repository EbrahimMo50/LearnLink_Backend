using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.MeetingsRepo
{
    public class MeetingRepo(AppDbContext dbContext) : IMeetingRepo
    {

        public async Task<MeetingModel> CreateMeetingAsync(MeetingModel meeting)
        {
            var result = await dbContext.Meetings.AddAsync(meeting);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public MeetingModel? GetById(int id)
        {
            return dbContext.Meetings.Include(x => x.Instructor).Include(x => x.Student).FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<MeetingModel>> GetMeetingsForInstructorAsync(string issuerId)
        {
            var result = await dbContext.Meetings
                .Where(x => x.StudentId == issuerId)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<MeetingModel>> GetMeetingsForStudentAsync(string issuerId)
        {
            var result = await dbContext.Meetings
             .Where(x => x.InstructorId == issuerId)
             .ToListAsync();
            return result;
        }

        public void Delete(int id)
        {
            var meeting = dbContext.Meetings.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException("could not find meeting");
            dbContext.Meetings.Remove(meeting);
            dbContext.SaveChanges();
        }

        public IEnumerable<MeetingModel> GetConflictingMeetings(string instructorId, int day)
        {
            return dbContext.Meetings
            .Include(x => x.Instructor)
            .Where(x => x.InstructorId.ToString() == instructorId)
            .Where(x => x.Day == day);
        }
    }
}
