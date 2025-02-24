using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;

namespace LearnLink_Backend.Repositories.SchedulesRepo
{
    public class InstructorScheduleRepo(AppDbContext dbContext) : IInstructorScheduleRepo
    {
        public Schedule CreateSchedule(Schedule schedule)
        {
            dbContext.Schedules.Add(schedule);
            dbContext.SaveChanges();
            return schedule;
        }

        public void DeleteSchedule(int id)
        {
            var schedule = dbContext.Schedules.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException("could not find schedule");
            dbContext.Schedules.Remove(schedule);
            dbContext.SaveChanges();
        }

        public Schedule? GetSchedule(int id)
        {
            return dbContext.Schedules.FirstOrDefault(x => x.Id == id);
        }

        public Schedule? GetScheduleByInstructorId(string id)
        {
            return dbContext.Schedules.FirstOrDefault(x => x.InstructorId.ToString() == id);
        }

        public Schedule UpdateSchedule(Schedule schedule)
        {
            dbContext.Schedules.Update(schedule);
            dbContext.SaveChanges();
            return schedule;
        }
    }
}
