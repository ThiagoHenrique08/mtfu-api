using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoreThanFollowUp.API.Controllers.Entities;
using MoreThanFollowUp.Application.DTO.Project_DTO;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Resources;
using MoreThanFollowUp.Infrastructure.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class PatchProjectsUnitTests
    {

        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IProject_UserRepository> _projectUserRepositoryMock;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepo;
        private readonly Mock<IProjectCategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProjectResponsibleRepository> _mockResponsibleRepo;
        private readonly ProjectController _controller;


        public PatchProjectsUnitTests()
        {
            // Configura UserManager para funcionar com Moq
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _projectUserRepositoryMock = new Mock<IProject_UserRepository>();
            _mockUserApplicationRepo = new Mock<IUserApplicationRepository>();
            _mockCategoryRepo = new Mock<IProjectCategoryRepository>();
            _mockResponsibleRepo = new Mock<IProjectResponsibleRepository>();
            _controller = new ProjectController(_projectRepositoryMock.Object, _userManagerMock.Object, _projectUserRepositoryMock.Object,
                                                     _mockUserApplicationRepo.Object, _mockCategoryRepo.Object, _mockResponsibleRepo.Object);
        }



        [Fact]
        public async Task PatchProject_ShouldReturnsNotFound_WhenProjectDTOIsNull()
        {
            // Arrange
            var projectRequest = new PATCHProjectDTO();
            projectRequest = null;

            // Act
            var result = await _controller.PatchProject(projectRequest!);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Verifica se o retorno é NotFound
        }

        [Fact]
        public async Task PatchProject_ShouldReturnNotFound_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectDTO = new PATCHProjectDTO { ProjectId = 1 };

            _projectRepositoryMock
                .Setup(repo => repo.RecuperarPorAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync((Project)null!);

            // Act
            var result = await _controller.PatchProject(projectDTO);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PatchProject_ShouldUpdateProject_WhenProjectExists()
        {
            // Arrange
            var projectDTO = new PATCHProjectDTO
            {
                ProjectId = 1,
                Title = "Updated Title"
            };

            var existingProject = new Project
            {
                ProjectId = 1,
                Title = "Old Title"
            };

            _projectRepositoryMock
                .Setup(repo => repo.RecuperarPorAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync(existingProject);

            // Act
            var result = await _controller.PatchProject(projectDTO);

            // Assert
            Assert.IsType<OkResult>(result);
            _projectRepositoryMock.Verify(repo => repo.AtualizarAsync(existingProject), Times.Once);
            Assert.Equal("Updated Title", existingProject.Title);
        }

        [Fact]
        public async Task PatchProject_ShouldReturnBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var projectDTO = new PATCHProjectDTO { ProjectId = 1 };

            _projectRepositoryMock
                .Setup(repo => repo.RecuperarPorAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.PatchProject(projectDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }




    }
}
