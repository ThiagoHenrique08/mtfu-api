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
using MoreThanFollowUp.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class PostProjectUnitTests
    {

        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IProject_UserRepository> _projectUserRepositoryMock;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepo;
        private readonly Mock<IProjectCategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProjectResponsibleRepository> _mockResponsibleRepo;
        private readonly ProjectController _controller;


        public PostProjectUnitTests()
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

        [Fact] //Testa se o método retorna Ok quando o projeto é criado com sucesso.
        public async Task PostProject_ReturnsOk_WhenProjectIsCreatedSuccessfully()
        {
            // Arrange
            var projectRequest = new RequestProjectDTO
            {
                Project = new POSTProjectDTO
                {
                    Title = "New Project",
                    Responsible = "John Doe",
                    Category = "Development",
                    Description = "A new project",
                    CreateDate = DateTime.Now
                },
                UsersList = new List<POSTUserToProjectDTO>
                {
                new POSTUserToProjectDTO { UserName = "User1" }
                }
            };

            var newProject = new Project
            {
                Title = "New Project",
                Responsible = "John Doe",
                Category = "Development",
                Description = "A new project",
                CreateDate = DateTime.Now
            };

            var applicationUser = new ApplicationUser { UserName = "User1" };

            // Configura as simulações
            _projectRepositoryMock.Setup(repo => repo.RecuperarPorAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync((Project?)null); // O projeto não existe ainda

            _projectRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Project>()));

            _userManagerMock.Setup(um => um.FindByNameAsync("User1"))
                .ReturnsAsync(applicationUser); // Usuário encontrado

            _projectUserRepositoryMock.Setup(repo => repo.CadastrarEmMassaAsync(It.IsAny<ICollection<Project_User>>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostProject(projectRequest);

            // Assert
            var okResult = Assert.IsType<OkResult>(result); // Verifica se o retorno é Ok
        }


        [Fact]//Verifica se o método retorna NotFound quando um dos usuários na lista não existe.
        public async Task PostProject_ReturnsNotFound_WhenUserNotExist()
        {
            // Arrange
            var projectRequest = new RequestProjectDTO
            {
                Project = new POSTProjectDTO
                {
                    Title = "New Project",
                    Responsible = "John Doe",
                    Category = "Development",
                    Description = "A new project",
                    CreateDate = DateTime.Now
                },
                UsersList = new List<POSTUserToProjectDTO>
                {
                new POSTUserToProjectDTO { UserName = "User1" }
                }
            };

            // Configura as simulações
            _projectRepositoryMock.Setup(repo => repo.RecuperarPorAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync((Project?)null); // O projeto não existe ainda

            _userManagerMock.Setup(um => um.FindByNameAsync("NonExistentUser"))
                .ReturnsAsync((ApplicationUser?)null); // Usuário não encontrado

            // Act
            var result = await _controller.PostProject(projectRequest);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not exist!", notFoundResult.Value); // Verifica se o retorno contém a mensagem correta

        }
        [Fact]
        public async Task PostProject_ReturnsNotFound_WhenProjectIsNull()
        {
            // Arrange
            var projectRequest = new RequestProjectDTO
            {
                Project = null,
                UsersList = new List<POSTUserToProjectDTO>()
            };

            // Act
            var result = await _controller.PostProject(projectRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Verifica se o retorno é NotFound
        }
    }
}
