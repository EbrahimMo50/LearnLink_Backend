using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Repositories.ApplicationsRepo;
using LearnLink_Backend.Services.AuthService;

namespace LearnLink_Backend.Services.ApplicationsService
{
    public class ApplicationService(IApplicationRepo applicationRepo, IAuthService authServices) : IApplicationService
    {
        public ApplicationModel ApplyForInstructor(ApplicationSet applicationSet)
        {
            if (applicationRepo.GetApplicationByEmail(applicationSet.Email) != null)
                throw new BadRequestException("application already exists");

            ApplicationModel application = new()
            {
                Name = applicationSet.Name,
                Email = applicationSet.Email,
                Password = applicationSet.Password,
                Messsage = applicationSet.Messsage,
                Nationality = applicationSet.Nationality,
                SpokenLanguage = applicationSet.SpokenLanguage,
                CreatedBy = "self"
            };

            return applicationRepo.CreateInstructorApplication(application);
        }

        public void DeleteApplication(int id)
        {
            applicationRepo.DeleteApplication(id);
        }

        public ApplicationGet GetApplicationById(int id)
        {
            var application = applicationRepo.GetApplicationById(id) ?? throw new NotFoundException("application not found");
            return ApplicationGet.ToDTO(application);
        }

        public IEnumerable<ApplicationGet> GetApplications()
        {
            return ApplicationGet.ToDTO(applicationRepo.GetApplications());
        }

        public void AcceptApplication(int id, string createrId)
        {
            var application = applicationRepo.GetApplicationById(id) ?? throw new NotFoundException("application was not found");

            Instructor instructor = new() { Name = application.Name, Email = application.Email, CreatedBy = createrId, Nationality = application.Nationality, SpokenLanguage = application.SpokenLanguage };
            authServices.SignInstructor(instructor, application.Password);
            applicationRepo.DeleteApplication(id);
        }
    }
}
