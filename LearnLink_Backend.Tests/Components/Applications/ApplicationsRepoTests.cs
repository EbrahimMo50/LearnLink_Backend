using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Repositories.ApplicationsRepo;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Tests.Components.Applications
{
    [TestClass]
    public class ApplicationsRepoTests
    {
        [TestMethod]
        public void CreateApplication_ValidInput_AnnouncementCreated() 
        {
            using (var context = GetTestDbContext("CreateApplication_ValidInput_AnnouncementCreated"))
            {
                var repo = new ApplicationRepo(context);
                repo.CreateInstructorApplication(GetDummyApplication());
                Assert.IsTrue(context.InstructorApplications.Any());
            }
        }
        [TestMethod]
        public void DeleteApplication_ValidInput_ApplicationDeleted()
        {
            using (var context = GetTestDbContext("DeleteApplication_ValidInput_ApplicationDeleted"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication());
                context.SaveChanges();
                repo.DeleteApplication(1);
                Assert.IsFalse(context.InstructorApplications.Any());
            }
        }
        [TestMethod]
        public void DeleteApplication_NonExistantId_NotFoundThrown()
        {
            using (var context = GetTestDbContext("DeleteApplication_NonExistantId_NotFoundThrown"))
            {
                var repo = new ApplicationRepo(context);
                Assert.ThrowsException<NotFoundException>(() => repo.DeleteApplication(2));
            }
        }
        [TestMethod]
        public void GetApplicationById_ValidInput_ApplicationReturned()
        {
            using (var context = GetTestDbContext("GetApplicationById_ValidInput_ApplicationReturned"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication());
                context.SaveChanges();
                var result = repo.GetApplicationById(1);
                Assert.IsNotNull(result);
                Assert.AreEqual("Test", result.CreatedBy);
            }
        }
        [TestMethod]
        public void GetApplicationById_NonExistantId_NullReturned()
        {
            using (var context = GetTestDbContext("GetApplicationById_NonExistantId_NullReturned"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication());
                context.SaveChanges();
                var result = repo.GetApplicationById(2);
                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public void GetApplicationByEmail_ValidInput_ApplicationReturned()
        {
            using (var context = GetTestDbContext("GetApplicationByEmail_ValidInput_ApplicationReturned"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication());
                context.SaveChanges();
                var result = repo.GetApplicationByEmail("Test");
                Assert.IsNotNull(result);
                Assert.AreEqual("Test", result.CreatedBy);
            }
        }
        [TestMethod]
        public void GetApplicationByEmail_NonExistantEmail_NullReturned()
        {
            using (var context = GetTestDbContext("GetApplicationByEmail_NonExistantEmail_NullReturned"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication());
                context.SaveChanges();
                var result = repo.GetApplicationByEmail("");
                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public void GetAllApplications_EmptyTable_EmptyArrayReturned()
        {
            using (var context = GetTestDbContext("GetAllApplications_EmptyTable_EmptyArrayReturned"))
            {
                var repo = new ApplicationRepo(context);
                var result = repo.GetApplications();
                Assert.IsNotNull(result);
                Assert.IsFalse(result.Any());
            }
        }
        [TestMethod]
        public void GetAllApplications_FilledTable_ApplicationsReturned()
        {
            using (var context = GetTestDbContext("GetAllApplications_FilledTable_ApplicationsReturned"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication("1"));
                context.InstructorApplications.Add(GetDummyApplication("2"));
                context.SaveChanges();

                var result = repo.GetApplications();
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count() == 2);
            }
        }
        [TestMethod]
        public void UpdateApplication_ValidUpdate_ApplicationUpdated()
        {
            using (var context = GetTestDbContext("UpdateApplication_ValidUpdate_ApplicationUpdated"))
            {
                var repo = new ApplicationRepo(context);
                context.InstructorApplications.Add(GetDummyApplication());
                context.SaveChanges();

                var application = context.InstructorApplications.First(x => x.Id == 1);
                application.Nationality = "New";
                repo.UpdateApplication(application);

                var updatedApplication = context.InstructorApplications.First(x => x.Id == 1);
                Assert.IsNotNull(updatedApplication);
                Assert.AreEqual("New", updatedApplication.Nationality);
            }
        }
        private static AppDbContext GetTestDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(name)
            .Options;
            return new AppDbContext(options);
        }
        private static ApplicationModel GetDummyApplication(string suffix = "")
        {
            return new ApplicationModel()
            {
                CreatedBy = "Test" + suffix,
                Name = "Test" + suffix,
                Nationality = "Test" + suffix,
                Email = "Test" + suffix,
                Messsage = "Test" + suffix,
                Password = "Test" + suffix,
                SpokenLanguage = "Test" + suffix
            };
        }
    }
}
