using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Meeting.Repos
{
    public class MeetingRepo(AppDbContext DbContext, IHttpContextAccessor httpContextAccess) : IMeetingRepo
    {
        public async Task<ResponseAPI> Create(MeetingSet meeting)
        {
            var student = await DbContext.Students.FirstOrDefaultAsync(x => x.Id.ToString() == meeting.StudentId);
            var instructor = await DbContext.Instructors.Include(x => x.Schedule).FirstOrDefaultAsync(x => x.Id.ToString() == meeting.InstructorId);
            string createrId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;

            if (student == null || instructor == null)
                return new ResponseAPI() { Message = "could not find user", StatusCode = 404 };

            if (meeting.StartsAt >= meeting.EndsAt)
                return new ResponseAPI() { Message = "confilct in time interval", StatusCode = 409 };

            if(instructor.FeesPerHour * (meeting.EndsAt - meeting.StartsAt) > student.Balance) 
                return new ResponseAPI() { Message = "Insufficient balance", StatusCode = 400};

            if(instructor.Schedule == null)
                return new ResponseAPI() { Message = "instructor didnt schedule time for meetings", StatusCode = 404 };

            if(instructor.Schedule.AvilableDays.Any(x => x == meeting.Day))
            {
                if(instructor.Schedule.StartHour <= meeting.StartsAt && instructor.Schedule.EndHour >= meeting.EndsAt)
                {
                    var possibleConfictingMeetings = DbContext.Meetings
                        .Include(x => x.Instructor)
                        .Where(x => x.InstructorId.ToString() == meeting.InstructorId)
                        .Where(x => x.Day == meeting.Day);

                    foreach (var m in possibleConfictingMeetings)
                        if (m.StartsAt <= meeting.StartsAt && m.EndsAt > meeting.StartsAt)
                            return new ResponseAPI() { Message = "instructor has a meeting this time", StatusCode = 400 };

                    student.Balance -= instructor.FeesPerHour * (meeting.EndsAt - meeting.StartsAt);
                    
                    MeetingModel meetingObject = new() {
                        CreatedBy = createrId, InstructorId = meeting.InstructorId, 
                        Instructor = instructor, StudentId = meeting.StudentId, Student = student,
                        Day = meeting.Day, StartsAt = meeting.StartsAt, EndsAt = meeting.EndsAt
                    };

                    await DbContext.Meetings.AddAsync(meetingObject);
                    await DbContext.SaveChangesAsync();
                    return new ResponseAPI() { Data = meetingObject };
                }
            }
            return new ResponseAPI() { Message = "no such schedule was found", StatusCode = 400 };
        }

        public ResponseAPI FindById(int id)
        {
            var result = DbContext.Meetings.FirstOrDefault(x => x.Id == id);
            if (result == null)
                return new ResponseAPI() { Message = "Could not find the searched result", StatusCode = 404 };

            return new ResponseAPI() { Data = MeetingGet.ToDTO(result) };
        }

        public async Task<ResponseAPI> FindMeetingsForInstructor()
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;  
            
            var result = await DbContext.Meetings
                .Where(x => x.StudentId == issuerId)
                .ToListAsync();
            return new ResponseAPI() { Data = MeetingGet.ToDTO(result) };
        }
        
        public async Task<ResponseAPI> FindMeetingsForStudent()
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;

            var result = await DbContext.Meetings
             .Where(x => x.InstructorId == issuerId)
             .ToListAsync();
            return new ResponseAPI() { Data = MeetingGet.ToDTO(result) };
        }

        public void Delete(int id)
        {
            string issuerId = httpContextAccess.HttpContext!.User.FindFirstValue("id")!;
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
