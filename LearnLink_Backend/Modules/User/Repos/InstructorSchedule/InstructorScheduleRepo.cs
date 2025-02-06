using LearnLink_Backend.Models;
using LearnLink_Backend.Services;

namespace LearnLink_Backend.Modules.User.Repos.UserSchedule
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
            var schedule = dbContext.Schedules.FirstOrDefault(x=>x.Id == id);
            if (schedule == null)
                return;
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
