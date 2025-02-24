using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.CoursesRepo
{
    public class CourseRepo(AppDbContext DbContext) : ICourseRepo
    {
        public async Task<CourseModel> CreateCourseAsync(CourseModel course)
        {
            var result = await DbContext.Courses.AddAsync(course);
            await DbContext.SaveChangesAsync();
            return result.Entity;
        }

        public IEnumerable<CourseModel> GetAllCourses()
        {
            return [.. DbContext.Courses.Include(x => x.Instructor)];
        }

        public async Task<CourseModel?> GetByIdAsync(int id)
        {
            var course = await DbContext.Courses.Include(x => x.Instructor)
                .Include(x => x.Students)
                .Include(x => x.Instructor)
                .Include(x => x.Announcements)
                .Include(x => x.Sessions)
                .FirstOrDefaultAsync(x => x.Id == id);
            return course;
        }

        public void Delete(int id)
        {
            var element = DbContext.Courses.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException("could not find course");
            DbContext.Courses.Remove(element);
            DbContext.SaveChanges();
        }

        public async Task<CourseModel> UpdateCourseAsync(CourseModel course)
        {
            var result = DbContext.Courses.Update(course);
            await DbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
