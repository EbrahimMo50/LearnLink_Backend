using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Repositories.UserMangementRepo
{
    public class UserRepo(AppDbContext dbContext) : IUserRepo
    {
        public Student AddStudent(Student student)
        {
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
            return student;
        }

        public Instructor AddInstructor(Instructor instructor)
        {
            dbContext.Instructors.Add(instructor);
            dbContext.SaveChanges();
            return instructor;
        }

        public Admin AddAdmin(Admin admin)
        {
            dbContext.Admins.Add(admin);
            dbContext.SaveChanges();
            return admin;
        }

        public Instructor? GetInstructorById(string id)
        {
            return dbContext.Instructors.Include(x => x.Courses).Include(x => x.Schedule).FirstOrDefault(x => x.Id.ToString() == id);
        }

        public Student? GetStudentById(string id)
        {
            return dbContext.Students.Include(x => x.Courses).FirstOrDefault(x => x.Id.ToString() == id);
        }

        public Admin? GetAdminById(string id)
        {
            return dbContext.Admins.FirstOrDefault(x => x.Id.ToString() == id);
        }


        public void DeleteInstructor(string id)
        {
            var instructor = dbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == id) ?? throw new NotFoundException("could not find instructor");
            dbContext.Instructors.Remove(instructor);
            dbContext.SaveChanges();
        }

        public void DeleteStudent(string id)
        {
            var student = dbContext.Students.FirstOrDefault(x => x.Id.ToString() == id) ?? throw new NotFoundException("could not find student");
            dbContext.Students.Remove(student);
            dbContext.SaveChanges();
        }

        public IEnumerable<Student> GetStudents(List<string>? ids)
        {
            if (ids == null)
                return [.. dbContext.Students];
            return dbContext.Students.Where(x => ids.Contains(x.Id.ToString()));
        }

        public IEnumerable<Instructor> GetInstructors(List<string>? ids)
        {
            if (ids == null)
                return [.. dbContext.Instructors];
            return dbContext.Instructors.Where(x => ids.Contains(x.Id.ToString()));
        }

        public Student UpdateStudent(Student student)
        {
            dbContext.Students.Update(student);
            dbContext.SaveChanges();
            return student;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            dbContext.Instructors.Update(instructor);
            dbContext.SaveChanges();
            return instructor;
        }

        public Student? GetStudentByEmail(string email)
        {
            return dbContext.Students.FirstOrDefault(x => x.Email == email);
        }

        public Instructor? GetInstructorByEmail(string email)
        {
            return dbContext.Instructors
                .Include(i => i.Schedule)
                .FirstOrDefault(x => x.Email == email);
        }

        public Admin? GetAdminByEmail(string email)
        {
            return dbContext.Admins.FirstOrDefault(x => x.Email == email);
        }
    }
}