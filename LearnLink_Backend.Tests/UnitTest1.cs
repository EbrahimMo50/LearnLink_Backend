
using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Announcement;
using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Moq;

namespace LearnLink_Backend.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("newData")
                .Options;

            var mockedDb = new Mock<AppDbContext>(options);
            var repo = new AnnouncementRepo(mockedDb.Object);
            var result = repo.CreateAnnouncement(new AnnouncementModel() { Title = "hello" });
            Assert.AreEqual(result.Result.Title, "hello");
        }
    }
}