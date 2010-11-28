using System.Web.Mvc;
using Moq;
using Note.Controllers;
using Note.Core.Repositories;
using Note.Core.Services;
using NUnit.Framework;
using Note.ViewModels;
namespace Note.Test
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IMembershipService> mockMembershipService;
        private Mock<IAuthenticationService> mockAuthenticationService;
        private Mock<IUserRepository> mockUserRepository;

        [SetUp]
        public void Setup()
        {
            mockMembershipService = new Mock<IMembershipService>();
            mockAuthenticationService = new Mock<IAuthenticationService>();
        }

        [Test]
        public void SignInShouldValidateAndAuthenticateUserWhenPostedTo()
        {
            mockMembershipService.Setup(ms => ms.ValidateUser("username", "password")).Returns(true);
            var userController = new UserController(mockAuthenticationService.Object, mockMembershipService.Object, mockUserRepository.Object);
            var model = new UserSignInViewModel { Password = "password", StaySignedIn = false, Username = "username" };

            userController.SignIn(model, null);

            mockMembershipService.Verify(ms => ms.ValidateUser("username", "password"), Times.Once());
            mockAuthenticationService.Verify(auth => auth.SignIn("username", false), Times.Once());
        }

        [Test]
        public void SignInShouldRedirectToReturnUrlWhenAuthenticationIsSuccessful()
        {
            mockMembershipService.Setup(ms => ms.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var userController = new UserController(mockAuthenticationService.Object, mockMembershipService.Object, mockUserRepository.Object);
            var model = new UserSignInViewModel { Password = "password", StaySignedIn = false, Username = "username" };

            var result = userController.SignIn(model, "/test/page") as RedirectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("/test/page", result.Url);
        }

        [Test]
        public void SignInShouldRedirectToNotesListWhenAuthenticationIsSuccessfulAndReturnUrlIsNotSpecified()
        {
            mockMembershipService.Setup(ms => ms.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var userController = new UserController(mockAuthenticationService.Object, mockMembershipService.Object, mockUserRepository.Object);
            var model = new UserSignInViewModel { Password = "password", StaySignedIn = false, Username = "username" };

            var result = userController.SignIn(model, null) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("list", result.RouteValues["action"]);
            Assert.AreEqual("notes", result.RouteValues["controller"]);
        }

    }
}