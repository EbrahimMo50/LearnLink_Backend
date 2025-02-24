using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;

namespace LearnLink_Backend.Repositories.ApplicationsRepo
{
    public class ApplicationRepo(AppDbContext dbContext) : IApplicationRepo
    {
        public ApplicationModel CreateInstructorApplication(ApplicationModel application)
        {
            var result = dbContext.InstructorApplications.Add(application);
            dbContext.SaveChanges();
            return result.Entity;
        }

        public void DeleteApplication(int id)
        {
            var application = dbContext.InstructorApplications.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException("could not find application");
            dbContext.Remove(application);
            dbContext.SaveChanges();
        }

        public ApplicationModel? GetApplicationById(int id)
        {
            return dbContext.InstructorApplications.FirstOrDefault(x => x.Id == id);
        }

        public ApplicationModel? GetApplicationByEmail(string email)
        {
            return dbContext.InstructorApplications.FirstOrDefault(x => x.Email == email);
        }

        public IEnumerable<ApplicationModel> GetApplications()
        {
            return [.. dbContext.InstructorApplications];
        }

        public ApplicationModel UpdateApplication(ApplicationModel application)
        {
            var result = dbContext.InstructorApplications.Update(application);
            dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
