using LearnLink_Backend.DTOs;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Services;
using System.Security.Claims;
using LearnLink_Backend.Models;
using Microsoft.EntityFrameworkCore;
using LearnLink_Backend.Exceptions;

namespace LearnLink_Backend.Modules.Courses.Repos
{
    public class CourseRepo(AppDbContext DbContext) : ICourseRepo
    {
        public async Task<CourseModel> CreateCourse(CourseModel course)
        {
            await DbContext.Courses.AddAsync(course);
            await DbContext.SaveChangesAsync();
            return course;
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            return [.. DbContext.Courses.Include(x => x.Instructor)];
        }

        public async Task<CourseModel> FindById(int id)
        {
            var course = await DbContext.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("could not find course");
            return course;
        }

        public void Delete(int id)
        {
            var element = DbContext.Courses.FirstOrDefault(x => x.Id == id);
            if(element == null)
                return;
            DbContext.Courses.Remove(element);
            DbContext.SaveChanges();
        }

        public async Task<CourseModel> UpdateCourse(int id, CourseSet course, string updaterId)
        {
            var updatedElement = await DbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            var instructor = await DbContext.Instructors.FirstOrDefaultAsync(x => x.Id.ToString() == course.InstructorId);
            updatedElement!.UpdatedBy = updaterId;
            updatedElement.UpdateTime = DateTime.Now;
            updatedElement.Name = course.Name;
            updatedElement.Instructor = instructor;

            await DbContext.SaveChangesAsync();
            return updatedElement;

        }
    }
}
