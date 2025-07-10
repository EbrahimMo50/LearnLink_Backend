using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.CoursesService
{
    public interface ICourseService
    {
        public Task<CourseModel> CreateCourseAsync(CourseSet course, string createrId);
        public IEnumerable<CourseGet> GetAllCourses();
        public Task<CourseGet> GetByIdAsync(int id);
        public Task DeleteAsync(int id, string issuerId);
        public Task<CourseModel> UpdateCourseAsync(int id, CourseSet course, string updaterId);
        public Task JoinCourseAsync(int courseId, string studentId);
        public Task LeaveCourseAsync(int courseId, string studentId);
        public IEnumerable<CourseGet> GetCoursesForInstructor(string instructorId);
    }
}
