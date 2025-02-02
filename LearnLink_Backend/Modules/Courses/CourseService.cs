using LearnLink_Backend.DTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Courses.Repos;
using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Courses
{
    public class CourseService(ICourseRepo repo,AppDbContext DbContext)
    {
        public async Task<CourseModel> CreateCourse(CourseSet course, string createrId)
        {
            Instructor? instructor = await DbContext.Instructors.FirstOrDefaultAsync(x => x.Id.ToString() == course.InstructorId);

            if (instructor == null)
                throw new NotFoundException("could not find specified instructor");

            CourseModel obj = new() { Name = course.Name, Instructor = instructor, CreatedBy = createrId, AtDate = DateTime.UtcNow };

            return await repo.CreateCourse(obj);
        }
        public IEnumerable<CourseGet> GetAllCourses()
        {
            return CourseGet.ToDTO(repo.GetAllCourses());
        }
        public async Task<CourseGet> FindById(int id)
        {
            return CourseGet.ToDTO(await repo.FindById(id));
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public async Task<CourseModel> UpdateCourse(int id, CourseSet course, string updaterId)
        {
            var updatedElement = await DbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            var instructor = await DbContext.Instructors.FirstOrDefaultAsync(x => x.Id.ToString() == course.InstructorId);

            if (instructor == null)
                throw new NotFoundException("could not find the instructor");

            if (updatedElement == null)
                throw new NotFoundException("could not find the course");

            if (updaterId == null)
                throw new BadRequestException("corrupted payload");

            return await repo.UpdateCourse(id, course, updaterId);
        }
        public async Task<string> JoinCourse(int courseId, string studentId)
        {
            if (studentId == null)
                throw new BadRequestException("corrupted payload");

            var student = await DbContext.Students.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            var course = await DbContext.Courses.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == courseId);
            if (student == null || course == null)
                throw new NotFoundException("could not find quered data");

            if (student.Courses.FirstOrDefault(x => x.Id == courseId) != null)
                throw new ConfilctException("student already registered");

            student.Courses.Add(course);
            course.Students.Add(student);

            student.UpdatedBy = studentId;
            student.UpdateTime = DateTime.UtcNow;

            await DbContext.SaveChangesAsync();

            return "joined succefully";
        }
        public async Task<string> LeaveCourse(string studentId, int courseId)
        {
            var student = await DbContext.Students.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id.ToString() == studentId);
            var course = await DbContext.Courses.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == courseId);
            if (student == null || course == null)
                throw new NotFoundException("could not find quered data");

            if (student.Courses.FirstOrDefault(x => x.Id == courseId) == null)
               throw new ConfilctException("student is not registered to this course");  //Conflict status code

            student.Courses.Remove(course);
            course.Students.Remove(student);

            student.UpdatedBy = studentId;
            student.UpdateTime = DateTime.UtcNow;

            await DbContext.SaveChangesAsync();

            return "left succefully";
        }
    }
}
