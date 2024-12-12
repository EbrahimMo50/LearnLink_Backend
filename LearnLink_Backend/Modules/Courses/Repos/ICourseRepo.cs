using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Courses.DTOs;

namespace LearnLink_Backend.Modules.Courses.Repos
{
    public interface ICourseRepo
    {
        public Task<ResponseAPI> CreateCourse(CourseSet course);
        public ResponseAPI GetAllCourses();
        public Task<ResponseAPI> FindById(int id);
        public void Delete(int id);
        public Task<ResponseAPI> UpdateCourse(int id, CourseSet course);
    }
}
