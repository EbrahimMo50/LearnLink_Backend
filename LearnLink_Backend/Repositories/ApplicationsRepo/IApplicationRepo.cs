using LearnLink_Backend.Entities;

namespace LearnLink_Backend.Repositories.ApplicationsRepo
{
    public interface IApplicationRepo
    {
        public ApplicationModel CreateInstructorApplication(ApplicationModel application);
        public ApplicationModel? GetApplicationById(int id);
        public ApplicationModel? GetApplicationByEmail(string email);
        public void DeleteApplication(int id);
        public IEnumerable<ApplicationModel> GetApplications();
        public ApplicationModel UpdateApplication(ApplicationModel application);
    }
}
