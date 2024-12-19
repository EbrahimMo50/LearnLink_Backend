using LearnLink_Backend.DTOs;
using LearnLink_Backend.DTOs.InstructorDTOs;
using LearnLink_Backend.DTOs.StudentDTOs;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Adminstration.DTOs;
using LearnLink_Backend.Modules.Authentcation;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnLink_Backend.Modules.Adminstration
{
    public class AdministrationService(AppDbContext DbContext, AuthServices authService, IHttpContextAccessor httpContextAccess)
    {
        public ResponseAPI AddAdmin(AdminSignVM adminAccount)
        {
            var createrId = httpContextAccess.HttpContext!.User.FindFirstValue("id");
            if (createrId == null)
                return new ResponseAPI() { Message = "corrupted payload", StatusCode = 400 };

            Admin admin = new() { Name = adminAccount.Name, Email = adminAccount.Email, CreatedBy = createrId };
            authService.SignAdmin(admin, adminAccount.Password);

            return new ResponseAPI() { Message = "added admin succefully" };
        }

        public async Task<ResponseAPI> GetApplications()
        {
            var result = await DbContext.InstructorApplications.ToListAsync();
            return new ResponseAPI() { Data = InstructorAppGet.ToDTO(result) };
        }

        public ResponseAPI GetAllStudents()
        {
            return new ResponseAPI() { Data = StudentGet.ToDTO([.. DbContext.Students]) };
        }

        public ResponseAPI GetAllInstructors()
        {
            return new ResponseAPI() { Data = InstructorGet.ToDTO([.. DbContext.Instructors]) };
        }
        public async Task<ResponseAPI> AcceptApplication(int id)
        {
            var createrId = httpContextAccess.HttpContext!.User.FindFirstValue("id");   //point is guarded with admin policy, thus the only reason we cant find the creater id in administration  is corruption
            var application = await DbContext.InstructorApplications.FirstOrDefaultAsync(x => x.Id == id);

            if (createrId == null)
                return new ResponseAPI() { Message = "corrupted payload", StatusCode = 400 };

            if (application == null)
                return new ResponseAPI() { Message = "could not find the application", StatusCode = 404 };

            Instructor instructor = new() { Name = application.Name, Email = application.Email, CreatedBy = createrId, Nationality = application.Nationality, SpokenLanguage = application.SpokenLanguage };
            authService.SignInstructor(instructor, application.Password);

            DbContext.InstructorApplications.Remove(application);
            await DbContext.SaveChangesAsync();

            return new ResponseAPI() { Message = "signed instructor succefully" };
        }

        // will not allow admins to delete each other
        public ResponseAPI RemoveUser(string id)
        {
            var student = DbContext.Students.FirstOrDefault(x => x.Id.ToString() == id);
            if (student != null){
                DbContext.Students.Remove(student);
                DbContext.SaveChanges();
                return new ResponseAPI() { Message = $"deleted student with id {id}" };
            }

            var instructor = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == id);
            if (instructor != null)
            {
                DbContext.Instructors.Remove(instructor);
                DbContext.SaveChanges();
                return new ResponseAPI() { Message = $"deleted instructor with id {id}" };
            }

            return new ResponseAPI() { Message = "could not find the user", StatusCode = 404 };
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