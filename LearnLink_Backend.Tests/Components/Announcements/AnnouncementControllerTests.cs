using LearnLink_Backend.Controllers;
using LearnLink_Backend.DTOs;
using LearnLink_Backend.Services.AnnouncementsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace LearnLink_Backend.Tests.Components.Announcements
{
    [TestClass]
    public class AnnouncementControllerTests
    {
        [TestMethod]
        public async Task CreateAnnouncement_IssuerMissing_BadRequestReturned()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockService = new Mock<IAnnouncementService>();
            var mockRequest = new Mock<HttpContext>();
            var user = new Mock<ClaimsPrincipal>();

            mockRequest.Setup(x => x.User).Returns(user.Object);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockRequest.Object);
            mockService.Setup(x => x.CreateAnnouncementAsync(It.IsAny<AnnouncementSet>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Entities.AnnouncementModel());

            var controller = new AnnouncementController(mockService.Object, mockHttpContextAccessor.Object);

            var result = await controller.CreateAnnouncement(new AnnouncementSet(), 1);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }
    }
}
