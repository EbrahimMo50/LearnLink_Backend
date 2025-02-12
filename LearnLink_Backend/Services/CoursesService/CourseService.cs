using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Repositories.CoursesRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;
using LearnLink_Backend.Services.NotificationsService;

namespace LearnLink_Backend.Services.CoursesService
{
    public class CourseService(ICourseRepo courseRepo, IUserRepo userRepo, INotificationService notificationService) : ICourseService
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

            var notification = new NotificationModel() { Reciever = course.Instructor!, Message = $"Student {student.Name} joined the {course.Name} course", Title = "a new student joined" };
            await notificationService.SendNotification(notification);
        }
        public async Task LeaveCourseAsync(int courseId, string studentId)
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

            var notification = new NotificationModel() { Reciever = course.Instructor!, Message = $"Student {student.Name} left the {course.Name} course", Title = "a student left the course" };
            await notificationService.SendNotification(notification);
        }
    }
}
