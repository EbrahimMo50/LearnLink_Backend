﻿using LearnLink_Backend.DTOs;
using LearnLink_Backend.Entities;
using LearnLink_Backend.Repositories.ApplicationsRepo;
using LearnLink_Backend.Services.ApplicationsService;
using LearnLink_Backend.Services.AuthService;
using Moq;

namespace LearnLink_Backend.Tests.Components.Applications
{
    [TestClass]
    public class ApplicationsServiceTests
    {
        [TestMethod]
        public void ApplyForInstructor_ValidApplication_ApplicationSaved()
        {
            var mockedAppRepo = new Mock<IApplicationRepo>();
            var mockedAuthService = new Mock<IAuthService>();
            mockedAppRepo.Setup(x => x.CreateInstructorApplication(It.IsAny<ApplicationModel>())).Returns(new ApplicationModel());

            var applicationsService = new ApplicationService(mockedAppRepo.Object, mockedAuthService.Object);
            var result = applicationsService.ApplyForInstructor(new ApplicationSet());

            Assert.IsNotNull(result);

        }
    }
}
