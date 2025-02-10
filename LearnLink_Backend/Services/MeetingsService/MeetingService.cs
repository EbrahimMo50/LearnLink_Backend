using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Repositories.MeetingsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;

namespace LearnLink_Backend.Services.MeetingsService
{
    public class MeetingService(IMeetingRepo meetingRepo, IUserRepo userRepo) : IMeetingService
    {
        public async Task<MeetingModel> CreateMeetingAsync(MeetingSet meeting, string createrId)
        {
            var student = userRepo.GetStudentById(meeting.StudentId);
            var instructor = userRepo.GetInstructorById(meeting.InstructorId);

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
                    var possibleConfictingMeetings = meetingRepo.GetConflictingMeetings(meeting.InstructorId, meeting.Day);

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

                    return await meetingRepo.CreateMeetingAsync(meetingObject);
                }
            }
            throw new NotFoundException("no such schedule was found");
        }
        public MeetingGet GetById(int id)
        {
            return MeetingGet.ToDTO(meetingRepo.GetById(id) ?? throw new NotFoundException("could not find course"));
        }
        public async Task<IEnumerable<MeetingGet>> GetMeetingsForInstructorAsync(string issuerId)
        {
            return MeetingGet.ToDTO(await meetingRepo.GetMeetingsForInstructorAsync(issuerId));
        }
        public async Task<IEnumerable<MeetingGet>> GetMeetingsForStudentAsync(string issuerId)
        {
            return MeetingGet.ToDTO(await meetingRepo.GetMeetingsForStudentAsync(issuerId));
        }
        public void Delete(int id, string issuerId)
        {
            meetingRepo.Delete(id, issuerId);
        }
    }
}
