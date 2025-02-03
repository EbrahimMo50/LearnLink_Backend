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
            .UseInMemoryDatabase("TestDatabase")
            .Options;

            using (var context = new AppDbContext(options)) // Context created inside using
            {
                var repo = new AnnouncementRepo(context);
                var result = repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello" });
                Assert.AreEqual(result.Result.Title, "hello");
            }
        }
    }
}
