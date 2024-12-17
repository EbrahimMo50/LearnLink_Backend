using LearnLink_Backend.DTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Repos;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Courses
{
    public class CourseService(ICourseRepo repo,AppDbContext DbContext)
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
        public async Task<ResponseAPI> JoinCourse(string studentId, int courseId)
        {
            var student = await DbContext.Students.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            var course = await DbContext.Courses.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == courseId);
            if (student == null || course == null)
                return new ResponseAPI() { Message = "could not find quered data" , StatusCode = 404};

            if(student.Courses.FirstOrDefault(x => x.Id == courseId) != null)
                return new ResponseAPI() { Message = "student already registered", StatusCode = 409};  //Conflict status code

            student.Courses.Add(course);
            course.Students.Add(student);
            await DbContext.SaveChangesAsync();

            return new ResponseAPI() { Message = "joined succefully", StatusCode = 200 };
        }
        public async Task<ResponseAPI> LeaveCourse(string studentId, int courseId)
        {
            var student = await DbContext.Students.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            var course = await DbContext.Courses.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == courseId);
            if (student == null || course == null)
                return new ResponseAPI() { Message = "could not find quered data", StatusCode = 404 };

            if (student.Courses.FirstOrDefault(x => x.Id == courseId) == null)
                return new ResponseAPI() { Message = "student is not registered to this course", StatusCode = 409 };  //Conflict status code

            student.Courses.Remove(course);
            course.Students.Remove(student);
            await DbContext.SaveChangesAsync();

            return new ResponseAPI() { Message = "left succefully", StatusCode = 200 };
        }
    }
}
