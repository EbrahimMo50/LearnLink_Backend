using LearnLink_Backend.Exceptions;
using Moq;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Repositories.AnnouncementsRepo;
using LearnLink_Backend.Repositories.CoursesRepo;
using LearnLink_Backend.Services.AnnouncementsService;
using LearnLink_Backend.DTOs;

namespace LearnLink_Backend.Tests.Components.Announcements
{
    [TestClass]
    public class AnnouncementServiceTests
    {
        [TestMethod]
        public async Task CreateAnnouncement_InvalidCourse_NotFoundExceptionThrown()
        {
            var announcementRepoMock = new Mock<IAnnouncementRepo>();
            var courseRepoMock = new Mock<ICourseRepo>();
            var service = new AnnouncementService(announcementRepoMock.Object, courseRepoMock.Object);
            await Assert.ThrowsExceptionAsync<NotFoundException>(
                async () => await service.CreateAnnouncementAsync(new AnnouncementSet() { Title = "Test" }, 1, "Test"));
        }

        [TestMethod]
        public async Task CreateAnnouncement_ValidInput_AnnouncementCreated()
        {
            var announcementRepoMock = new Mock<IAnnouncementRepo>();
            var courseRepoMock = new Mock<ICourseRepo>();

            var expectedCourse = new CourseModel { };
            var announcemnt = new AnnouncementModel { Title = "Test", Description = "Test", CourseId = 1, CreatedBy = "Test" };

            courseRepoMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedCourse);
            announcementRepoMock.Setup(repo => repo.CreateAnnouncementAsync(It.IsAny<AnnouncementModel>())).ReturnsAsync(announcemnt);

            var service = new AnnouncementService(announcementRepoMock.Object, courseRepoMock.Object);
            var createdAnnouncement = await service.CreateAnnouncementAsync(new AnnouncementSet() { Title = "Title Test", Description = "Description Test" }, 1, "Test");

            Assert.AreEqual("Test", createdAnnouncement.Title);
        }

        [TestMethod]
        public void GetAllForCourse_ValidCourseId_AnnouncementsReturned()
        {
            var announcementRepoMock = new Mock<IAnnouncementRepo>();
            var courseRepoMock = new Mock<ICourseRepo>();
            announcementRepoMock.Setup(x => x.GetAllForCourse(1)).Returns
                ([
                new AnnouncementModel() { Title = "Title Test", CreatedBy = "CreatedBy Test", Description = "Description Test", CourseId = 1 },
                new AnnouncementModel() { Title = "Title Test", CreatedBy = "CreatedBy Test", Description = "Description Test", CourseId = 1 }
                ]);
            var service = new AnnouncementService(announcementRepoMock.Object, courseRepoMock.Object);
            var announcements = service.GetAllForCourse(1);
            Assert.AreEqual(2, announcements.Count());
        }

        [TestMethod]
        public void GetAllForCourse_EmptyAnnouncementCourseId_EmptyListReturned()
        {
            var announcementRepoMock = new Mock<IAnnouncementRepo>();
            var courseRepoMock = new Mock<ICourseRepo>();
            var service = new AnnouncementService(announcementRepoMock.Object, courseRepoMock.Object);
            var announcements = service.GetAllForCourse(1);
            Assert.AreEqual(0, announcements.Count());        
        }

        [TestMethod]
        public async Task GetById_ValidId_AnnouncementReturned()
        {
            var announcementRepoMock = new Mock<IAnnouncementRepo>();
            var courseRepoMock = new Mock<ICourseRepo>();
            announcementRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new AnnouncementModel() { Title = "Test" });
            var service = new AnnouncementService(announcementRepoMock.Object, courseRepoMock.Object);
            var result = await service.GetByIdAsync(1);
            Assert.AreEqual("Test", result.Title);
        }
    }
}

