using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Models;

namespace LearnLink_Backend.Modules.Courses.Repos
{
    public interface ICourseRepo
    {
        public Task<CourseModel> CreateCourseAsync(CourseModel course);
        public IEnumerable<CourseModel> GetAllCourses();
        public Task<CourseModel?> GetByIdAsync(int id);
        public void Delete(int id);
        public Task<CourseModel> UpdateCourseAsync(CourseModel course);
    }
}
