using LearnLink_Backend.Modules.Applications.DTOs;

namespace LearnLink_Backend.Modules.Applications.Services
{
    public interface IApplicationService
    {
        public ApplicationModel ApplyForInstructor(ApplicationSet applicationSet);
        public void DeleteApplication(int id);
        public ApplicationGet GetApplicationById(int id);
        public IEnumerable<ApplicationGet> GetApplications();
        public void AcceptApplication(int id, string createrId);
    }
}
