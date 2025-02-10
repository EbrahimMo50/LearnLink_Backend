using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Services.ApplicationsService
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
