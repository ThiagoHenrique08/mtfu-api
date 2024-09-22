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
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;

namespace MoreThanFollowUp.Tests.UnitTests.Authentication
{
    public class PostCreateRoleUnitTests
    {
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepository;
        private readonly AuthController _controller;

        public PostCreateRoleUnitTests()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>(); // Necessário para o RoleManager
            _mockTokenService = new Mock<ITokenService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null
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
        public async Task CreateRole_ShouldReturnOk_WhenRoleIsCreatedSuccessfully()
        {
            // Arrange
            var roleName = "Admin";
            _mockRoleManager.Setup(x => x.RoleExistsAsync(roleName)).ReturnsAsync(false);
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.CreateRole(roleName);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var response = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.Equal("Success", response.Status);
            Assert.Equal($"Role {roleName} added successfully", response.Message);
        }

        [Fact]
        public async Task CreateRole_ShouldReturnBadRequest_WhenRoleAlreadyExists()
        {
            // Arrange
            var roleName = "Admin";
            _mockRoleManager.Setup(x => x.RoleExistsAsync(roleName)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateRole(roleName);

            // Assert
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var response = Assert.IsType<ResponseDTO>(badRequestResult.Value);
            Assert.Equal("Error", response.Status);
            Assert.Equal("Role already exist.", response.Message);
        }

        [Fact]
        public async Task CreateRole_ShouldReturnBadRequest_WhenRoleCreationFails()
        {
            // Arrange
            var roleName = "Admin";
            _mockRoleManager.Setup(x => x.RoleExistsAsync(roleName)).ReturnsAsync(false);
            _mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _controller.CreateRole(roleName);

            // Assert
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var response = Assert.IsType<ResponseDTO>(badRequestResult.Value);
            Assert.Equal("Error", response.Status);
            Assert.Equal($"Issue adding the new {roleName} role", response.Message);
        }
    }
}
