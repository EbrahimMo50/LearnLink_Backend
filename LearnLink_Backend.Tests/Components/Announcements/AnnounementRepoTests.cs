using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Tests.Components.Announcements
{
    [TestClass]
    public class AnnounementRepoTests
    {
        [TestMethod]
        public void CreateAnnouncement_ValidInput_AnnouncementCreated()     // following the [UnitOfWork_StateUnderTest_ExpectedBehavior] convention
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("CreateAnnouncement_ValidInput_AnnouncementCreated")
            .Options;

            using (var context = new AppDbContext(options)) 
            {
                var repo = new AnnouncementRepo(context);
                repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello" }).Wait();
                Assert.AreNotEqual(context.Announcements.FirstOrDefault(), null);
            }
        }

        [TestMethod]
        public void ReadAnnouncement_ValidId_AnnouncementReturned()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("ReadAnnouncement_ValidId_AnnouncementReturned")
            .Options;

            using (var context = new AppDbContext(options))
            {
                var repo = new AnnouncementRepo(context);
                var result = repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello" }).Result;
                var announcement = repo.FindById(result.Id).Result;
                Assert.AreEqual(announcement.Id, result.Id);
            }
        }

        [TestMethod]
        public void GetAllForCourse_ValidCourseId_AnnouncementsReturned()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("GetAllForCourse_ValidCourseId_AnnouncementsReturned")
            .Options;

            using (var context = new AppDbContext(options))
            {
                var repo = new AnnouncementRepo(context);
                var result = repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello", CourseId = 1 }).Result;
                var announcements = repo.GetAllForCourse(1);
                Assert.AreEqual(announcements.Count(), 1);
            }
        }

        [TestMethod]
        public void UpdateAnnouncement_ValidInput_AnnouncementUpdated()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("UpdateAnnouncement_ValidInput_AnnouncementUpdated")
            .Options;

            using (var context = new AppDbContext(options))
            {
                var repo = new AnnouncementRepo(context);
                var result = repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello" }).Result;
                result.Title = "hello updated";
                var updated = repo.UpdateAnnouncement(1,new Modules.Announcement.DTOs.AnnouncementUpdate() { Description = "hello updated"},"self").Result;
                Assert.AreEqual(updated.Description, "hello updated");
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
                context.Announcements.Add(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello" });
                repo.DeleteAnnouncement(1);
                Assert.AreEqual(context.Announcements.Count(),0);
            }
        }

        [TestMethod]
        public void DeleteAnnouncement_InvalidInput_AnnouncementNotDeleted()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DeleteAnnouncement_InvalidInput_AnnouncementNotDeleted")
            .Options;

            using (var context = new AppDbContext(options))
            {
                var repo = new AnnouncementRepo(context);
                var result = repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello" }).Result;
                repo.DeleteAnnouncement(2);
                Assert.IsTrue(context.Announcements.Any());
            }
        }
    }
}
