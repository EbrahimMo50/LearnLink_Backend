using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Repositories.CoursesRepo
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
