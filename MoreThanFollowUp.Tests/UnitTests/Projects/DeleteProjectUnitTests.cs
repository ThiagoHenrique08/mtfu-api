using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoreThanFollowUp.API.Controllers.Entities;
using MoreThanFollowUp.Application.DTO.Project;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using MoreThanFollowUp.Infrastructure.Repository.Models;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class DeleteProjectUnitTests 
    {

        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IProject_UserRepository> _projectUserRepositoryMock;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepo;
        private readonly Mock<IProjectCategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProjectResponsibleRepository> _mockResponsibleRepo;
        private readonly Mock<IProjectStatusRepository> _mockStatusRepositoryMock;
        private readonly Mock<IPlanningRepository> _PlanningRepositoryMock;
        private readonly Mock<IEnterpriseRepository> _EnterpriseRepository;
        private readonly ProjectController _controller;


        public DeleteProjectUnitTests()
        {
            // Configura UserManager para funcionar com Moq
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _projectUserRepositoryMock = new Mock<IProject_UserRepository>();
            _mockUserApplicationRepo = new Mock<IUserApplicationRepository>();
            _mockCategoryRepo = new Mock<IProjectCategoryRepository>();
            _mockResponsibleRepo = new Mock<IProjectResponsibleRepository>();
            _mockStatusRepositoryMock = new Mock<IProjectStatusRepository>();
            _PlanningRepositoryMock = new Mock<IPlanningRepository>();
            _EnterpriseRepository= new Mock<IEnterpriseRepository>();
            _controller = new ProjectController(_projectRepositoryMock.Object, _userManagerMock.Object, _projectUserRepositoryMock.Object,
                                                     _mockUserApplicationRepo.Object, _mockCategoryRepo.Object, _mockResponsibleRepo.Object,
                                                        _mockStatusRepositoryMock.Object, _PlanningRepositoryMock.Object, _EnterpriseRepository.Object);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnNotFound_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectDTO = new Project { ProjectId = Guid.NewGuid(), Title = "teste" };

            _projectRepositoryMock
                .Setup(repo => repo.RecoverBy(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync((Project)null!);

            // Act
            var result = await _controller.DeleteProject(projectDTO.ProjectId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProject_ShouldDeleteProject_WhenProjectExists()
        {
            // Arrange

            var existingProject = new Project
            {
                ProjectId = Guid.NewGuid(),
                Title = "Old Title"
            };

            var auxId = existingProject.ProjectId;

            _projectRepositoryMock
                .Setup(repo => repo.RecoverBy(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync(existingProject);

            // Act
            var result = await _controller.DeleteProject(existingProject.ProjectId);

            // Assert
            Assert.IsType<OkResult>(result);
            _projectRepositoryMock.Verify(repo => repo.DeleteAsync(existingProject), Times.Once);
            Assert.Equal(auxId, existingProject.ProjectId);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var projectDTO = new PATCHProjectDTO { ProjectId = Guid.NewGuid() };

            _projectRepositoryMock
                .Setup(repo => repo.RecoverBy(It.IsAny<Expression<Func<Project, bool>>>()))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.DeleteProject(projectDTO.ProjectId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }
    }
}
