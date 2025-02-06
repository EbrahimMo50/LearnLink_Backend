using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Courses.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Courses.Repos;
using LearnLink_Backend.Modules.User.Repos.UserMangement;

namespace LearnLink_Backend.Modules.Courses
{
    public class CourseService(ICourseRepo courseRepo, IUserRepo userRepo)
    {
        public async Task<CourseModel> CreateCourseAsync(CourseSet course, string createrId)
        {
            Instructor instructor = userRepo.GetInstructorById(course.InstructorId) ?? throw new NotFoundException("could not find specified instructor");

            CourseModel obj = new() { Name = course.Name, Instructor = instructor, CreatedBy = createrId, AtDate = DateTime.UtcNow };

            return await courseRepo.CreateCourseAsync(obj);
        }
        public IEnumerable<CourseGet> GetAllCourses()
        {
            return CourseGet.ToDTO(courseRepo.GetAllCourses());
        }
        public async Task<CourseGet> GetByIdAsync(int id)
        {
            return CourseGet.ToDTO(await courseRepo.GetByIdAsync(id) ?? throw new NotFoundException("course could not be found"));
        }
        public void Delete(int id)
        {
            courseRepo.Delete(id);
        }
        public async Task<CourseModel> UpdateCourseAsync(int id, CourseSet course, string updaterId)
        {
            var updatedElement = await courseRepo.GetByIdAsync(id);
            var instructor = userRepo.GetInstructorById(course.InstructorId);

            if (instructor == null)
                throw new NotFoundException("could not find the instructor");

            if (updatedElement == null)
                throw new NotFoundException("could not find the course");

            updatedElement.Name = course.Name;
            updatedElement.Instructor = instructor;
            updatedElement.UpdatedBy = updaterId;
            updatedElement.UpdateTime = DateTime.UtcNow;

            return await courseRepo.UpdateCourseAsync(updatedElement);
        }
        public async Task JoinCourseAsync(int courseId, string studentId)
        {
            var student = userRepo.GetStudentById(studentId);
            var course = await courseRepo.GetByIdAsync(courseId);

            if (student == null || course == null)
                throw new NotFoundException("could not find quered data");

            if (student.Courses.FirstOrDefault(x => x.Id == courseId) != null)
                throw new ConfilctException("student already registered");

            student.Courses.Add(course);
            course.Students.Add(student);

            student.UpdatedBy = studentId;
            student.UpdateTime = DateTime.UtcNow;

            userRepo.UpdateStudent(student);
        }
        public async Task LeaveCourseAsync(string studentId, int courseId)
        {
            var student = userRepo.GetStudentById(studentId);
            var course = await courseRepo.GetByIdAsync(courseId);
            if (student == null || course == null)
                throw new NotFoundException("could not find quered data");

            if (student.Courses.FirstOrDefault(x => x.Id == courseId) == null)
               throw new ConfilctException("student is not registered to this course"); 

            student.Courses.Remove(course);
            course.Students.Remove(student);

            student.UpdatedBy = studentId;
            student.UpdateTime = DateTime.UtcNow;

            userRepo.UpdateStudent(student);
            await courseRepo.UpdateCourseAsync(course);
        }
    }
}
