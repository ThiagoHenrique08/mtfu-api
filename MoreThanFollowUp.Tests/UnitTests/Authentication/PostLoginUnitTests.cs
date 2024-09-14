using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MoreThanFollowUp.API.Controllers.Authentication;
using MoreThanFollowUp.API.Interfaces;
using MoreThanFollowUp.Application.DTO.Login;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Tests.UnitTests.Authentication
{
    public class PostLoginUnitTests
    {
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepository;
        private readonly AuthController _controller;

        public PostLoginUnitTests()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>(); // Necessário para o UserManager

            _mockTokenService = new Mock<ITokenService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null
            );
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _mockUserApplicationRepository = new Mock<IUserApplicationRepository>();

            _controller = new AuthController(
                _mockTokenService.Object,
                _mockUserManager.Object,
                null,
                _mockConfiguration.Object,
                _mockLogger.Object,
                _mockUserApplicationRepository.Object
            );
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenLoginSuccessful()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "testuser",
                Password = "password123"
            };

            var user = new ApplicationUser
            {
                UserName = loginModel.Username,
                Email = "testuser@test.com"
            };

            var token = new JwtSecurityToken();
            var refreshToken = "refreshToken123";

            _mockUserManager.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, loginModel.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Admin" });
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            _mockTokenService.Setup(x => x.GenerateAccessToken(It.IsAny<List<Claim>>(), _mockConfiguration.Object)).Returns(token);
            _mockTokenService.Setup(x => x.GenerateRefreshToken()).Returns(refreshToken);

            _mockConfiguration.Setup(x => x["JWT:RefreshTokenValidityInMinutes"]).Returns("120");

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenPasswordIsIncorrect()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "testuser",
                Password = "wrongpassword"
            };

            var user = new ApplicationUser
            {
                UserName = loginModel.Username,
                Email = "testuser@test.com"
            };

            _mockUserManager.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(user, loginModel.Password)).ReturnsAsync(false);

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Username = "nonexistentuser",
                Password = "password123"
            };

            _mockUserManager.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
