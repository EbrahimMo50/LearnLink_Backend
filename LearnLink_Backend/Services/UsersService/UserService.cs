﻿using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Repositories.SchedulesRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;

namespace LearnLink_Backend.Services.UsersService
{
    public class UserService(IUserRepo userRepo, IInstructorScheduleRepo scheduleRepo) : IUserService
    {
        public InstructorScheduleGet UpdateSchedule(ScheduleSet scheduleSet, string initiatorId)
        {
            var instructor = userRepo.GetInstructorById(initiatorId) ?? throw new NotFoundException("could not find instructor");
            var schedule = scheduleRepo.GetScheduleByInstructorId(instructor.Id.ToString()) ?? throw new NotFoundException("could not find schedule");

            schedule.AvilableDays = scheduleSet.AvilableDays;
            schedule.EndHour = scheduleSet.EndHour;
            schedule.InstructorId = instructor.Id;
            schedule.StartHour = scheduleSet.StartHour;
            schedule.CreatedBy = initiatorId;

            return InstructorScheduleGet.ToDTO(scheduleRepo.UpdateSchedule(schedule));
        }
        public StudentGet AddBalance(string id, decimal balance, string updaterId)
        {
            var student = userRepo.GetStudentById(id) ?? throw new NotFoundException("could not find user");
            student.UpdatedBy = updaterId;
            student.UpdateTime = DateTime.UtcNow;
            student.Balance += balance;
            return StudentGet.ToDTO(userRepo.UpdateStudent(student));
        }

        public IEnumerable<StudentGet> GetStudents(List<string> ids)
        {
            return StudentGet.ToDTO(userRepo.GetStudents(ids));
        }

        public IEnumerable<InstructorGet> GetInstructors(List<string> ids)
        {
            return InstructorGet.ToDTO(userRepo.GetInstructors(ids));
        }
    }
}
