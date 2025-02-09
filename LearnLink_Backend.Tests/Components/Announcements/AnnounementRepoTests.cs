using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

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
        public void DeleteAnnouncement_InvalidInput_AnnouncementNotDeleted()
        {
            using (var context = GetTestDbContext("DeleteAnnouncement_InvalidInput_AnnouncementNotDeleted"))
            {
                var repo = new AnnouncementRepo(context);
                context.Announcements.Add(new AnnouncementModel() {Title = "Test", CreatedBy = "Test", Description = "Test" });
                context.SaveChanges();
                repo.DeleteAnnouncement(2);
                Assert.IsTrue(context.Announcements.Any());
            }
        }
    }
}
