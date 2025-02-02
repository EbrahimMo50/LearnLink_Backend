using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Meeting.DTOs;
using LearnLink_Backend.Modules.Meeting.Repos;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Meeting
{
    public class MeetingService(IMeetingRepo repo, AppDbContext DbContext)
    {
        public async Task<MeetingModel> Create(MeetingSet meeting, string createrId)
        {
            var student = await DbContext.Students.FirstOrDefaultAsync(x => x.Id.ToString() == meeting.StudentId);
            var instructor = await DbContext.Instructors.Include(x => x.Schedule).FirstOrDefaultAsync(x => x.Id.ToString() == meeting.InstructorId);

            if (student == null || instructor == null)
                throw new NotFoundException("could not find user");

            if (meeting.StartsAt >= meeting.EndsAt)
                throw new ConfilctException("confilct in time interval");

            if (instructor.FeesPerHour * (meeting.EndsAt - meeting.StartsAt) > student.Balance)
                throw new BadRequestException("Insufficient balance");

            if (instructor.Schedule == null)
                throw new NotFoundException("instructor didnt schedule time for meetings");

            if (instructor.Schedule.AvilableDays.Any(x => x == meeting.Day))
            {
                if (instructor.Schedule.StartHour <= meeting.StartsAt && instructor.Schedule.EndHour >= meeting.EndsAt)
                {
                    var possibleConfictingMeetings = DbContext.Meetings
                        .Include(x => x.Instructor)
                        .Where(x => x.InstructorId.ToString() == meeting.InstructorId)
                        .Where(x => x.Day == meeting.Day);

                    foreach (var m in possibleConfictingMeetings)
                        if (m.StartsAt <= meeting.StartsAt && m.EndsAt > meeting.StartsAt)
                            throw new BadRequestException("confilcting meeting");

                    student.Balance -= instructor.FeesPerHour * (meeting.EndsAt - meeting.StartsAt);

                    MeetingModel meetingObject = new()
                    {
                        CreatedBy = createrId,
                        InstructorId = meeting.InstructorId,
                        Instructor = instructor,
                        StudentId = meeting.StudentId,
                        Student = student,
                        Day = meeting.Day,
                        StartsAt = meeting.StartsAt,
                        EndsAt = meeting.EndsAt
                    };

                    return await repo.Create(meetingObject);
                }
            }
            throw new NotFoundException("no such schedule was found");
        }
        public MeetingGet FindById(int id)
        {
            return MeetingGet.ToDTO(repo.FindById(id));
        }
        public async Task<IEnumerable<MeetingGet>> FindMeetingsForInstructor(string issuerId)
        {
            return MeetingGet.ToDTO(await repo.FindMeetingsForInstructor(issuerId));
        }
        public async Task<IEnumerable<MeetingGet>> FindMeetingsForStudent(string issuerId)
        {
            return MeetingGet.ToDTO(await repo.FindMeetingsForStudent(issuerId));
        }
        public void Delete(int id,string issuerId)
        {
            repo.Delete(id, issuerId);
        }
    }
}
