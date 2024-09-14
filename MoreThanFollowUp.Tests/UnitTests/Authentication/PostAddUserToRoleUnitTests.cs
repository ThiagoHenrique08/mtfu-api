using Microsoft.AspNetCore.Http;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoreThanFollowUp.Tests.UnitTests.Authentication
{
    public class PostAddUserToRoleUnitTests
    {
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepository;
        private readonly AuthController _controller;

        public PostAddUserToRoleUnitTests()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>(); // Necessário para o RoleManager
            var userStore = new Mock<IUserStore<ApplicationUser>>(); // Necessário para o UserManager

            _mockTokenService = new Mock<ITokenService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null
            );
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null
            );
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _mockUserApplicationRepository = new Mock<IUserApplicationRepository>();

            _controller = new AuthController(
                _mockTokenService.Object,
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockConfiguration.Object,
                _mockLogger.Object,
                _mockUserApplicationRepository.Object
            );
        }

        [Fact]
        public async Task AddUserToRole_ShouldReturnOk_WhenUserAddedToRoleSuccessfully()
        {
            // Arrange
            var email = "test@test.com";
            var roleName = "Admin";
            var user = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.AddUserToRole(email, roleName);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var response = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Success", response.Status);
            Assert.Equal($"User {email} to the {roleName} role", response.Message);
        }

        [Fact]
        public async Task AddUserToRole_ShouldReturnBadRequest_WhenUserNotFound()
        {
            // Arrange
            var email = "test@test.com";
            var roleName = "Admin";

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.AddUserToRole(email, roleName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            var error = new { error = "Unable to find user" };

            Assert.Equal(error.ToString(), badRequestResult.Value!.ToString());
        }

        [Fact]
        public async Task AddUserToRole_ShouldReturnBadRequest_WhenAddToRoleFails()
        {
            // Arrange
            var email = "test@test.com";
            var roleName = "Admin";
            var user = new ApplicationUser { Email = email };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _controller.AddUserToRole(email, roleName);

            // Assert
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var response = Assert.IsType<ResponseDTO>(badRequestResult.Value);
            Assert.Equal("Error", response.Status);
            Assert.Equal($"Error: Unable to add user {email} to the {roleName} role", response.Message);
        }
    }
}
