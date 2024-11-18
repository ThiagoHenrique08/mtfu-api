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
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoreThanFollowUp.Tests.UnitTests.Authentication
{
    public class PostAddUserToRoleUnitTests
    {
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<ApplicationRole>> _mockRoleManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepository;
        private readonly Mock<IApplicationUserRoleEnterpriseRepository> _mockUserRoleEnterpriseRepository;
        private readonly Mock<IEnterpriseRepository> _mockEnterpriseRepository;
        private readonly Mock<ITenantRepository> _mockTenantRepository;
        private readonly Mock<IEnterprise_UserRepository> _mockEnterpriseUserRepository;
        private readonly AuthController _controller;

        public PostAddUserToRoleUnitTests()
        {
            var roleStore = new Mock<IRoleStore<ApplicationRole>>(); // Necessário para o RoleManager
            var userStore = new Mock<IUserStore<ApplicationUser>>(); // Necessário para o UserManager

            _mockTokenService = new Mock<ITokenService>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null
            );
            _mockRoleManager = new Mock<RoleManager<ApplicationRole>>(
                roleStore.Object, null, null, null, null
            );
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _mockUserApplicationRepository = new Mock<IUserApplicationRepository>();
            _mockEnterpriseRepository = new Mock<IEnterpriseRepository>();
            _mockUserRoleEnterpriseRepository = new Mock<IApplicationUserRoleEnterpriseRepository>();
            _mockTenantRepository = new Mock<ITenantRepository>();
            _mockEnterpriseUserRepository = new Mock<IEnterprise_UserRepository>();
            _controller = new AuthController(
                _mockTokenService.Object,
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockConfiguration.Object,
                _mockLogger.Object,
                _mockUserApplicationRepository.Object,
                _mockUserRoleEnterpriseRepository.Object,
                _mockEnterpriseRepository.Object,
                _mockTenantRepository.Object,
                _mockEnterpriseUserRepository.Object
            );
        }
        [Fact]
        public async Task AddUserToRole_UserAddedToRole_Success()
        {
            // Arrange
            var email = "test@example.com";
            var roleName = "Admin";
            var enterpriseId = Guid.NewGuid();
            var user = new ApplicationUser { Id = "1", Email = email };
            var role = new ApplicationRole { Id = "1", Name = roleName };
            var enterprise = new Enterprise { EnterpriseId = enterpriseId, CorporateReason = "Test Corp" };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockRoleManager.Setup(x => x.FindByNameAsync(roleName)).ReturnsAsync(role);
            _mockEnterpriseRepository.Setup(x => x.RecoverBy(It.IsAny<Expression<Func<Enterprise,bool>>>())).ReturnsAsync(enterprise);
            _mockUserRoleEnterpriseRepository.Setup(x => x.RegisterAsync(It.IsAny<ApplicationUserRoleEnterprise>())).ReturnsAsync(new ApplicationUserRoleEnterprise());

            // Act
            var result = await _controller.AddUserToRoleToEnterprise(email, roleName, enterpriseId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<ResponseDTO>(result.Value);
            var response = (ResponseDTO)result.Value;
            Assert.Equal("Success", response.Status);
            Assert.Equal($"User {email} to the {roleName} role and {enterprise.CorporateReason}", response.Message);
        }

        [Fact]
        public async Task AddUserToRole_ShouldReturnBadRequest_WhenUserNotFound()
        {
            // Arrange
            var email = "test@test.com";
            var roleName = "Admin";
            var enterpriseId = Guid.NewGuid();
            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.AddUserToRoleToEnterprise(email, roleName, enterpriseId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            var error = new { error = "Unable to find user" };

            Assert.Equal(error.ToString(), badRequestResult.Value!.ToString());
        }


        [Fact]
        public async Task AddUserToRole_UserNotAddedToRole_Error()
        {
            // Arrange
            var email = "test@example.com";
            var roleName = "Admin";
            var enterpriseId = Guid.NewGuid();
            var user = new ApplicationUser { Id = "1", Email = email };
            var role = new ApplicationRole { Id = "1", Name = roleName };
            var enterprise = new Enterprise { EnterpriseId = enterpriseId, CorporateReason = "Test Corp" };

            _mockUserManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockRoleManager.Setup(x => x.FindByNameAsync(roleName)).ReturnsAsync(role);
            _mockEnterpriseRepository.Setup(x => x.RecoverBy(It.IsAny<Expression<Func<Enterprise, bool>>>())).ReturnsAsync(enterprise);
            _mockUserRoleEnterpriseRepository.Setup(x => x.RegisterAsync(It.IsAny<ApplicationUserRoleEnterprise>()))!.ReturnsAsync((ApplicationUserRoleEnterprise)null!);

            // Act
            var result = await _controller.AddUserToRoleToEnterprise(email, roleName, enterpriseId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsType<ResponseDTO>(result.Value);
            var response = (ResponseDTO)result.Value;
            Assert.Equal("Error", response.Status);
            Assert.Equal($"Error: Unable to add user {email} to the {roleName} role and {enterprise.CorporateReason}", response.Message);
        }
    }
}
