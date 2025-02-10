using LearnLink_Backend.Entities;
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

        public void Delete(int id, string issuerId)
        {
            var meeting = dbContext.Meetings.Include(x => x.Student).Include(x => x.Instructor).FirstOrDefault(x => x.Id == id);
            if (meeting == null)
                return;

            if (issuerId == meeting.StudentId || issuerId == meeting.InstructorId || dbContext.Admins.Any(x => x.Id.ToString() == issuerId))
            {
                var student = meeting.Student;
                student.Balance += meeting.Instructor.FeesPerHour * (meeting.EndsAt - meeting.StartsAt);
                dbContext.Meetings.Remove(meeting);
                dbContext.SaveChanges();
                return;
            }
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
