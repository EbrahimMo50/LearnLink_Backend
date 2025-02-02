using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Modules.Authentcation;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Adminstration
{
    public class AdministrationService(AppDbContext DbContext, AuthServices authService)
    {
        public string AddAdmin(AdminSignVM adminAccount, string createrId)
        {
            if (createrId == null)
                throw new BadRequestException("corrupted payload");

            Admin admin = new() { Name = adminAccount.Name, Email = adminAccount.Email, CreatedBy = createrId };
            authService.SignAdmin(admin, adminAccount.Password);

            return "added admin succefully";
        }

        public async Task<IEnumerable<InstructorAppGet>> GetApplications()
        {
            var result = await DbContext.InstructorApplications.ToListAsync();
            return InstructorAppGet.ToDTO(result);
        }

        public IEnumerable<StudentGet> GetAllStudents()
        {
            return StudentGet.ToDTO([.. DbContext.Students]);
        }

        public IEnumerable<InstructorGet> GetAllInstructors()
        {
            return InstructorGet.ToDTO([.. DbContext.Instructors]);
        }
        public async Task<string> AcceptApplication(int id, string createrId)
        {
            var application = await DbContext.InstructorApplications.FirstOrDefaultAsync(x => x.Id == id);

            if (createrId == null)
                throw new BadRequestException("corrupted payload");

            if (application == null)
                throw new NotFoundException("could not find the application");

            Instructor instructor = new() { Name = application.Name, Email = application.Email, CreatedBy = createrId, Nationality = application.Nationality, SpokenLanguage = application.SpokenLanguage };
            authService.SignInstructor(instructor, application.Password);

            DbContext.InstructorApplications.Remove(application);
            await DbContext.SaveChangesAsync();

            return "signed instructor succefully";
        }

        // will not allow admins to delete each other
        public string RemoveUser(string id)
        {
            var student = DbContext.Students.FirstOrDefault(x => x.Id.ToString() == id);
            if (student != null){
                DbContext.Students.Remove(student);
                DbContext.SaveChanges();
                return $"deleted student with id {id}";
            }

            var instructor = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == id);
            if (instructor != null)
            {
                DbContext.Instructors.Remove(instructor);
                DbContext.SaveChanges();
                return $"deleted instructor with id {id}";
            }

            throw new NotFoundException("could not find the user");
        }

        public void DeleteApplication(int id)
        {
            var application = DbContext.InstructorApplications.FirstOrDefault(x => x.Id == id);
            if (application != null)
                DbContext.InstructorApplications.Remove(application);
            DbContext.SaveChanges();
        }
    }
}