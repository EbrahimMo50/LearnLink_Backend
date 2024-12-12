using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Repos;

namespace LearnLink_Backend.Modules.Courses
{
    public class CourseService(ICourseRepo repo)
    {
        public Task<ResponseAPI> CreateCourse(CourseSet course)
        {
            return repo.CreateCourse(course);
        }
        public ResponseAPI GetAllCourses()
        {
            return repo.GetAllCourses();
        }
        public Task<ResponseAPI> FindById(int id)
        {
            return repo.FindById(id);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public Task<ResponseAPI> UpdateCourse(int id, CourseSet course)
        {
            return repo.UpdateCourse(id, course);
        }
    }
}
