using LearnLink_Backend.Entities;
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Repositories.AnnouncementsRepo;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Tests.Components.Announcements
{
    [TestClass]
    public class AnnounementRepoTests
    {
        private static AppDbContext GetTestDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(name)
            .Options;
            return new AppDbContext(options);
        }
        [TestMethod]
        public async Task CreateAnnouncementAsync_ValidInput_AnnouncementCreated()
        {
            using (var context = GetTestDbContext("CreateAnnouncementAsync_ValidInput_AnnouncementCreated"))
            {
                var repo = new AnnouncementRepo(context);
                await repo.CreateAnnouncementAsync(new AnnouncementModel() { Title = "Test", CreatedBy = "Test", Description = "Test" });
                Assert.IsTrue(context.Announcements.Any());
            }
        }
        [TestMethod]
        public async Task GetByIdAsync_ValidId_AnnouncementReturned()
        {
            using (var context = GetTestDbContext("GetByIdAsync_ValidId_AnnouncementReturned"))
            {
                var repo = new AnnouncementRepo(context);
                context.Announcements.Add(new AnnouncementModel() { Title = "Test" , CreatedBy = "Test", Description = "Test"});
                context.SaveChanges();
                var announcement = await repo.GetByIdAsync(1);
                Assert.AreEqual(announcement!.Title, "Test");
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_InValidId_NullReturned()
        {
            using (var context = GetTestDbContext("GetByIdAsync_InValidId_NullReturned"))
            {
                var repo = new AnnouncementRepo(context);
                var announcement = await repo.GetByIdAsync(1);
                Assert.IsNull(announcement);
            }
        }

        [TestMethod]
        public void GetAllForCourse_ValidCourseId_AnnouncementsReturned()
        {
            using (var context = GetTestDbContext("GetAllForCourse_ValidCourseId_AnnouncementsReturned"))
            {
                var repo = new AnnouncementRepo(context);
                context.Announcements.Add(new AnnouncementModel() { Title = "Test", CreatedBy = "Test", Description = "Test", CourseId = 1 });
                context.SaveChanges();
                var announcements = repo.GetAllForCourse(1);
                Assert.AreEqual(1, announcements.Count());
            }
        }

        [TestMethod]
        public async Task UpdateAnnouncement_ValidInput_AnnouncementUpdated()
        {
            using (var context = GetTestDbContext("UpdateAnnouncement_ValidInput_AnnouncementUpdated"))
            {
                var repo = new AnnouncementRepo(context);
                var announcement = new AnnouncementModel() { Title = "Test", CreatedBy = "Test", Description = "Test" };
                announcement.Title = "Test Update";
                var updatedAnnouncement = await repo.UpdateAnnouncementAsync(announcement);
                Assert.AreEqual("Test Update", updatedAnnouncement.Title);
            }
        }

        [TestMethod]
        public void DeleteAnnouncement_ValidInput_AnnouncementDeleted()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteAnnouncement_ValidInput_AnnouncementDeleted")
            .Options;

            using (var context = new AppDbContext(options))
            {
                var repo = new AnnouncementRepo(context);
                context.Announcements.Add(new AnnouncementModel() {Title = "Test", CreatedBy = "Test", Description = "Test" });
                context.SaveChanges();
                repo.DeleteAnnouncement(1);
                Assert.IsFalse(context.Announcements.Any());
            }
        }

        [TestMethod]
        public void DeleteAnnouncement_NonExistantAnnouncement_NotFoundExceptionThrown()
        {
            using (var context = GetTestDbContext("DeleteAnnouncement_NonExistantAnnouncement_NotFoundExceptionThrown"))
            {
                var repo = new AnnouncementRepo(context);
                context.Announcements.Add(new AnnouncementModel() {Title = "Test", CreatedBy = "Test", Description = "Test" });
                context.SaveChanges();
                Assert.ThrowsException<NotFoundException>(() => repo.DeleteAnnouncement(2));
            }
        }
    }
}
