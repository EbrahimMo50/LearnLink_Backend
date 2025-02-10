using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Courses.Services
{
    public interface ICourseService
    {
        public Task<CourseModel> CreateCourseAsync(CourseSet course, string createrId);
        public IEnumerable<CourseGet> GetAllCourses();
        public Task<CourseGet> GetByIdAsync(int id);
        public void Delete(int id);
        public Task<CourseModel> UpdateCourseAsync(int id, CourseSet course, string updaterId);
        public Task JoinCourseAsync(int courseId, string studentId);
        public Task LeaveCourseAsync(string studentId, int courseId);
    }
}
