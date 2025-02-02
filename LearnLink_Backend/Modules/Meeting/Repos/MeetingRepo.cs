using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Meeting.Repos
{
    public class MeetingRepo(AppDbContext DbContext) : IMeetingRepo
    {

        public async Task<MeetingModel> Create(MeetingModel meeting)
        {
            await DbContext.Meetings.AddAsync(meeting);
            await DbContext.SaveChangesAsync();
            return meeting;
        }

        public MeetingModel FindById(int id)
        {
            var result = DbContext.Meetings.FirstOrDefault(x => x.Id == id);
            if (result == null)
                throw new NotFoundException("Could not find the searched result");

            return result;
        }

        public async Task<IEnumerable<MeetingModel>> FindMeetingsForInstructor(string issuerId)
        {   
            var result = await DbContext.Meetings
                .Where(x => x.StudentId == issuerId)
                .ToListAsync();
            return result;
        }
        
        public async Task<IEnumerable<MeetingModel>> FindMeetingsForStudent(string issuerId)
        {
            var result = await DbContext.Meetings
             .Where(x => x.InstructorId == issuerId)
             .ToListAsync();
            return result;
        }

        public void Delete(int id, string issuerId)
        {
            //string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
            var meeting = DbContext.Meetings.Include(x => x.Student).Include(x => x.Instructor).FirstOrDefault(x => x.Id == id);
            if (meeting == null)
                return;

            if (issuerId == meeting.StudentId || issuerId == meeting.InstructorId || DbContext.Admins.Any(x => x.Id.ToString() == issuerId))
            {    
                var student = meeting.Student;
                student.Balance += meeting.Instructor.FeesPerHour * (meeting.EndsAt - meeting.StartsAt);
                DbContext.Meetings.Remove(meeting);
                DbContext.SaveChanges();
                return;
            }
        }
    }
}
