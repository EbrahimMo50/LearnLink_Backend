using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Models;
using LearnLink_Backend.Modules.Applications.DTOs;
using LearnLink_Backend.Modules.Applications.Repos;
using LearnLink_Backend.Modules.Authentcation;

namespace LearnLink_Backend.Modules.Applications
{
    public class ApplicationService(IApplicationRepo applicationRepo, AuthServices authServices)
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
