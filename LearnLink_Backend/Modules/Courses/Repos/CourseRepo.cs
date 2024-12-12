using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Services;
using System.Security.Claims;
using LearnLink_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Modules.Courses.Repos
{
    public class CourseRepo(AppDbContext DbContext, IHttpContextAccessor httpContextAccess) : ICourseRepo
    {
        public async Task<ResponseAPI> CreateCourse(CourseSet course)
        {
            if (httpContextAccess.HttpContext == null || httpContextAccess.HttpContext.User.FindFirstValue("id") == null)
                return new ResponseAPI() { Message = "Error in handling the context of the request" , StatusCode = 401 };

            string createrId = httpContextAccess.HttpContext.User.FindFirstValue("id")!;
            Instructor? instructor = await DbContext.Instructors.FirstOrDefaultAsync(x => x.Id.ToString() == course.InstructorId);

            if (instructor == null)
                return new ResponseAPI() { StatusCode = 404, Message = "could not find specified instructor" };

            CourseModel obj = new() { Name = course.Name, Instructor = instructor , CreatedBy = createrId, AtDate = DateTime.UtcNow };
            await DbContext.Courses.AddAsync(obj);
            await DbContext.SaveChangesAsync();
            return new ResponseAPI() { Message = "Added Succefully", Data = obj };
        }

        public ResponseAPI GetAllCourses()
        {
            return new ResponseAPI() { Data = CourseGet.ToDTO([.. DbContext.Courses.Include(x => x.Instructor)]) };
        }

        public async Task<ResponseAPI> FindById(int id)
        {
            var course = await DbContext.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == id);

            if (course == null)
                return new ResponseAPI() { Message = "could not find course", StatusCode = 404 };

            return new ResponseAPI() { Data = CourseGet.ToDTO(course) };
        }

        public void Delete(int id)
        {
            var element = DbContext.Courses.FirstOrDefault(x => x.Id == id);
            if(element == null)
                return;
            DbContext.Courses.Remove(element);
            DbContext.SaveChanges();
        }

        public async Task<ResponseAPI> UpdateCourse(int id, CourseSet course)
        {
            var updatedElement = await DbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            var instructor = await DbContext.Instructors.FirstOrDefaultAsync(x => x.Id.ToString() == course.InstructorId);
            string? updaterId = httpContextAccess.HttpContext!.User.FindFirstValue("id");

            if (instructor == null)
                return new ResponseAPI() { Message = "could not find the instructor", StatusCode = 404 };

            if (updatedElement == null)
                return new ResponseAPI() { Message = "could not find the element" , StatusCode = 404};

            if (updaterId == null)
                return new ResponseAPI() { Message = "context of http is corrupted", StatusCode = 400 };

            updatedElement.UpdatedBy = updaterId;
            updatedElement.UpdateTime = DateTime.Now;
            updatedElement.Name = course.Name;
            updatedElement.Instructor = instructor;

            await DbContext.SaveChangesAsync();
            return new ResponseAPI() { Data = updatedElement };

        }
    }
}
