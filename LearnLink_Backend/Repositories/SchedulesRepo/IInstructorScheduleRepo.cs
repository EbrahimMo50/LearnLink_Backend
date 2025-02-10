using LearnLink_Backend.Models;

namespace LearnLink_Backend.Repositories.SchedulesRepo
{
    public interface IInstructorScheduleRepo
    {
        public Schedule CreateSchedule(Schedule schedule);
        public Schedule UpdateSchedule(Schedule schedule);
        public void DeleteSchedule(int id);
        public Schedule? GetSchedule(int id);
        public Schedule? GetScheduleByInstructorId(string id);
    }
}
