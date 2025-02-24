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
        public async Task CreateAnnouncement_IdClaimMissing_BadRequestReturned()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockService = new Mock<IAnnouncementService>();
            var mockRequest = new Mock<HttpContext>();
            var user = new Mock<ClaimsPrincipal>();

            mockRequest.Setup(x => x.User).Returns(user.Object);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockRequest.Object);

            var controller = new AnnouncementController(mockService.Object, mockHttpContextAccessor.Object);

            var result = await controller.CreateAnnouncement(new AnnouncementSet(), 1);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }
        [TestMethod]
        public async Task CreateAnnouncement_ValidInput_CreatedStateReturned()
        {
            var mockService = new Mock<IAnnouncementService>();

            mockService.Setup(x => x.CreateAnnouncementAsync(It.IsAny<AnnouncementSet>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new Entities.AnnouncementModel());

            var controller = new AnnouncementController(mockService.Object, GetValidMockedContextAccessorObject());

            var result = await controller.CreateAnnouncement(new AnnouncementSet(), 1);
            Assert.IsInstanceOfType<CreatedAtRouteResult>(result);
        }
        [TestMethod]
        public void GetAllForCourse_ValidInput_OkStatusReturned()
        {
            var mockedService = new Mock<IAnnouncementService>();
            var mockedHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockedService.Setup(x => x.GetAllForCourse(It.IsAny<int>())).Returns([
                new AnnouncementGet() { Title = "Test1"},
                new AnnouncementGet() { Title = "Test2" }
                ]);

            var controller = new AnnouncementController(mockedService.Object, mockedHttpContextAccessor.Object);
            var result = controller.GetAllForCourse(1) as OkObjectResult;
            Assert.IsInstanceOfType<OkObjectResult>(result);
            Assert.AreNotEqual(result.Value, "{}");         //checks if the value of the json is not empty
        }
        [TestMethod]
        public async Task UpdateAnnouncement_IdClaimMissing_BadRequestReturned()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockService = new Mock<IAnnouncementService>();
            var mockRequest = new Mock<HttpContext>();
            var user = new Mock<ClaimsPrincipal>();

            mockRequest.Setup(x => x.User).Returns(user.Object);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockRequest.Object);

            var controller = new AnnouncementController(mockService.Object, mockHttpContextAccessor.Object);

            var result = await controller.UpdateAnnouncement(1, new AnnouncementUpdate());
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }
        [TestMethod]
        public async Task UpdateAnnouncement_ValidInput_OkStatusRetunred()
        {
            var mockService = new Mock<IAnnouncementService>();
            mockService.Setup(x => x.UpdateAnnouncementAsync(It.IsAny<int>(), It.IsAny<AnnouncementUpdate>(), It.IsAny<string>()))
                .ReturnsAsync(new Entities.AnnouncementModel());

            var controller = new AnnouncementController(mockService.Object, GetValidMockedContextAccessorObject());

            var result = await controller.UpdateAnnouncement(1, new AnnouncementUpdate());
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }
        /// <summary>
        /// returns a context with claims principle to pass the id requirment in the controllers
        /// </summary>
        /// <returns></returns>
        private static IHttpContextAccessor GetValidMockedContextAccessorObject()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockRequest = new Mock<HttpContext>();

            var claims = new List<Claim> { new("id", "123") };
            var identity = new ClaimsIdentity(claims, "id");
            var user = new ClaimsPrincipal(identity);

            mockRequest.Setup(x => x.User).Returns(user);
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockRequest.Object);

            return mockHttpContextAccessor.Object;
        }
    }
}
