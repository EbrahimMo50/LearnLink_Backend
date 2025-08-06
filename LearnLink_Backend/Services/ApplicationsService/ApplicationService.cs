using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Repositories.ApplicationsRepo;
using LearnLink_Backend.Repositories.UserMangementRepo;
using LearnLink_Backend.Services.AuthService;

namespace LearnLink_Backend.Services.ApplicationsService
{
    public class ApplicationService(IApplicationRepo applicationRepo, IAuthService authServices, IUserRepo userRepo) : IApplicationService
    {
        public ApplicationModel ApplyForInstructor(ApplicationSet applicationSet)
        {
            if (applicationRepo.GetApplicationByEmail(applicationSet.Email) != null)
                throw new BadRequestException("application already exists");

            if (userRepo.GetInstructorByEmail(applicationSet.Email) is null)
                throw new ConfilctException("email is already used");

            ApplicationModel application = new()
            {
                Name = applicationSet.Name,
                Email = applicationSet.Email,
                Password = applicationSet.Password,
                Message = applicationSet.Messsage,
                Nationality = applicationSet.Nationality,
                SpokenLanguages = applicationSet.SpokenLanguages.ToArray(),
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

            // TODO fix spoken languages
            Instructor instructor = new() { Name = application.Name, Email = application.Email, CreatedBy = createrId, Nationality = application.Nationality, SpokenLanguages = [] };
            authServices.SignInstructor(instructor, application.Password);
            applicationRepo.DeleteApplication(id);
        }
    }
}
