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

            if (meeting.StartDate >= meeting.EndDate)
                throw new ConfilctException("confilct in time interval");

            var durationInHours = (decimal)(meeting.EndDate - meeting.StartDate).TotalHours;

            if (instructor.FeesPerHour * durationInHours > student.Balance)
                throw new BadRequestException("Insufficient balance");

            if (instructor.Schedule == null)
                throw new NotFoundException("instructor didnt schedule time for meetings");

            var scheduleAtDay = instructor.Schedule
            .FirstOrDefault(x => x.Day == meeting.StartDate.DayOfWeek) ?? throw new BadRequestException("instructor does not have this day listed");

            var validInterval = scheduleAtDay.Intervals
                .FirstOrDefault(x => x.Start <= TimeOnly.FromDateTime(meeting.StartDate) && x.End >= TimeOnly.FromDateTime(meeting.EndDate));
           
            var possibleConfictingMeetings = meetingRepo
                .GetConflictingMeetings(meeting.InstructorId, DateOnly.FromDateTime(meeting.StartDate));

            foreach (var m in possibleConfictingMeetings)
            {
                if (meeting.StartDate < m.EndDate && m.StartDate < meeting.EndDate)
                    throw new BadRequestException("conflicting meeting");
            }

            student.Balance -= instructor.FeesPerHour * durationInHours;

            MeetingModel meetingObject = new()
            {
                CreatedBy = createrId,
                InstructorId = meeting.InstructorId,
                Instructor = instructor,
                StudentId = meeting.StudentId,
                Student = student,
                StartDate = meeting.StartDate,
                EndDate = meeting.EndDate,
            };

            userRepo.UpdateStudent(student);
            return await meetingRepo.CreateMeetingAsync(meetingObject);

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
            var meeting = meetingRepo.GetById(id) ?? throw new NotFoundException("meeting not found");
            var student = meeting.Student;
            if(meeting.StudentId == issuerId || meeting.InstructorId == issuerId || userRepo.GetAdminById(issuerId) != null)
            {
                var durationInHours = (decimal)(meeting.EndDate - meeting.StartDate).TotalHours;
                student.Balance += meeting.Instructor.FeesPerHour * durationInHours;
                userRepo.UpdateStudent(student);
                meetingRepo.Delete(id);
            }
        }
    }
}
