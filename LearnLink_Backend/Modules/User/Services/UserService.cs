using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.Models;
using LearnLink_Backend.Modules.User.DTOs;
using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.User.Services
{
    public class UserService(AppDbContext DbContext)
    {
        public Schedule UpdateSchedule(ScheduleSet scheduleSet, string initiatorId)
        {
            var instructor = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == initiatorId);

            if (instructor == null)
                throw new NotFoundException("could not find instructor");

            Schedule schedule = new()
            {
                AvilableDays = scheduleSet.AvilableDays,
                EndHour = scheduleSet.EndHour,
                InstructorId = instructor.Id,
                StartHour = scheduleSet.StartHour,
                CreatedBy = initiatorId,
            };

            DbContext.Schedules.Add(schedule);
            DbContext.SaveChanges();
            return schedule;
        }
        public Student AddBalance(string id, decimal balance, string updaterId)
        {
            var student = DbContext.Students.FirstOrDefault(x => x.Id.ToString() == id) ?? throw new NotFoundException("could not find user");
            student.UpdatedBy = updaterId;
            student.UpdateTime = DateTime.UtcNow;
            student.Balance += balance;
            DbContext.SaveChanges();
            return student;
        }
        public string ApplyForInstructor(InstructorAppSet applicationSet)
        {
            if (DbContext.InstructorApplications.Any())
                throw new BadRequestException("application already exists");
            
            InstructorApplicationModel application = new()
            {
                Name = applicationSet.Name,
                Email = applicationSet.Email,
                Password = applicationSet.Password,
                Messsage = applicationSet.Messsage,
                Nationality = applicationSet.Nationality,
                SpokenLanguage = applicationSet.SpokenLanguage,
                CreatedBy = "self"
            };
            DbContext.InstructorApplications.Add(application);
            DbContext.SaveChanges();
            return"succefully applied";
        }

        internal IActionResult GetStudents(List<string> ids)
        {
            throw new NotImplementedException();
        }

        internal IActionResult GetInstructors(List<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
