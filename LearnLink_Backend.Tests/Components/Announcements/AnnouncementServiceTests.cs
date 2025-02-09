using LearnLink_Backend.Exceptions;
using LearnLink_Backend.Modules.Announcement.Repo;
using LearnLink_Backend.Modules.Announcement;
using Moq;
using LearnLink_Backend.Modules.Announcement.DTOs;
using LearnLink_Backend.Modules.Courses.Models;
using LearnLink_Backend.Modules.Courses.Repos;

namespace LearnLink_Backend.Tests.Components.Announcements
{
    [TestClass]
    public class AnnouncementServiceTests
    {
        [TestMethod]
        public async Task CreateAnnouncement_InvalidCourse_NotFoundExceptionThrown()
        {
            var mockedAnnouncementRepo = new Mock<IAnnouncementRepo>();
            var mockedCourseRepo = new Mock<ICourseRepo>();
            var service = new AnnouncementService(mockedAnnouncementRepo.Object, mockedCourseRepo.Object);
            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await service.CreateAnnouncementAsync(new AnnouncementSet() { Title = "Test" }, 1, "Test"));
        }

        [TestMethod]
        public async Task CreateAnnouncement_ValidInput_AnnouncementCreated()
        {
            var mockedAnnouncmentRepo = new Mock<IAnnouncementRepo>();
            var mockedCourseRepo = new Mock<ICourseRepo>();

            var expectedCourse = new CourseModel { Id = 1 };
            var announcemnt = new AnnouncementModel { Title = "Test", Description = "Test", CourseId = 1, CreatedBy = "Test" };

            mockedCourseRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedCourse);
            mockedAnnouncmentRepo.Setup(repo => repo.CreateAnnouncementAsync(It.IsAny<AnnouncementModel>())).ReturnsAsync(announcemnt);
            var service = new AnnouncementService(mockedAnnouncmentRepo.Object, mockedCourseRepo.Object);
            var createdAnnouncement = await service.CreateAnnouncementAsync(new AnnouncementSet() { Title = "Test", Description = "Test" }, 1, "Test");
            Assert.AreEqual("fail", createdAnnouncement.Title);
        }

        //[TestMethod]
        //public void GetAllForCourse_ValidCourseId_AnnouncementsReturned()
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //                    .UseInMemoryDatabase("GetAllForCourse_ValidCourseId_AnnouncementsReturned")
        //                    .Options;
        //    var context = new Mock<AppDbContext>(options);

        //    var repo = new Mock<IAnnouncementRepo>();
        //    repo.Setup(x => x.GetAllForCourse(It.IsAny<int>())).Returns
        //        ([
        //        new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello", CourseId = 1 },
        //        new AnnouncementModel() { Title = "hello2", CreatedBy = "self", Description = "this is hello2", CourseId = 1 }
        //        ]);
        //    var service = new AnnouncementService(repo.Object, context.Object);
        //    var announcements = service.GetAllForCourse(1);
        //    Assert.AreEqual(announcements.Count(), 2);
        //}

        //[TestMethod]
        //public void GetAllForCourse_EmptyCourseId_EmptyListReturned()
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //                    .UseInMemoryDatabase("GetAllForCourse_EmptyCourseId_EmptyListReturned")
        //                    .Options;

        //    using (var context = new AppDbContext(options))
        //    {
        //        var repo = new Mock<IAnnouncementRepo>();
        //        var service = new AnnouncementService(repo.Object, context);
        //        var Course = new CourseModel() { Name = "Course", CreatedBy = "self" };
        //        context.Courses.Add(Course);
        //        context.SaveChanges();
        //        var announcements = service.GetAllForCourse(1);
        //        Assert.AreEqual(announcements.Count(), 0);
        //        context.Dispose();
        //    }
        //}

        //[TestMethod]
        //public async Task FindById_ValidId_AnnouncementReturned()
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //                    .UseInMemoryDatabase("FindById_ValidId_AnnouncementReturned")
        //                    .Options;

        //    var context = new AppDbContext(options);
        //    var repo = new AnnouncementRepo(context);
        //    context.Announcements.Add(new AnnouncementModel() { Title = "hello", CreatedBy = "self", Description = "this is hello", CourseId = 1 });
        //    context.SaveChanges();
        //    var service = new AnnouncementService(repo, context);
        //    var result = await service.FindById(1);
        //    Assert.AreEqual(result.Title, "hello");
        //}
    }
}

