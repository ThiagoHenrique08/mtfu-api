using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoreThanFollowUp.API.Controllers.Entities;
using MoreThanFollowUp.Application.DTO.Project_DTO;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using System.Linq.Expressions;

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
        private readonly Mock<IProjectStatusRepository> _mockStatusRepositoryMock;
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
            _mockStatusRepositoryMock = new Mock<IProjectStatusRepository>();
            _controller = new ProjectController(_projectRepositoryMock.Object, _userManagerMock.Object, _projectUserRepositoryMock.Object,
                                                     _mockUserApplicationRepo.Object, _mockCategoryRepo.Object, _mockResponsibleRepo.Object, _mockStatusRepositoryMock.Object);
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
                    
                },
                UsersList = new List<POSTUserToProjectDTO>
                {
                new POSTUserToProjectDTO { CompletedName = "User1" }
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

            var applicationUser = new ApplicationUser { CompletedName = "User1" };

            // Configura as simulações
            _projectRepositoryMock.Setup(repo => repo.RecoverBy(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync((Project?)null); // O projeto não existe ainda

            _projectRepositoryMock.Setup(repo => repo.RegisterAsync(It.IsAny<Project>()));

            _mockUserApplicationRepo.Setup(um => um.RecoverBy(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).ReturnsAsync(applicationUser);   


            _projectUserRepositoryMock.Setup(repo => repo.RegisterList(It.IsAny<ICollection<Project_User>>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProject(projectRequest);

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
              
                },
                UsersList = new List<POSTUserToProjectDTO>
                {
                new POSTUserToProjectDTO { CompletedName = "User1" }
                }
            };

            // Configura as simulações
            _projectRepositoryMock.Setup(repo => repo.RecoverBy(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync((Project?)null); // O projeto não existe ainda

            _userManagerMock.Setup(um => um.FindByNameAsync("NonExistentUser"))
                .ReturnsAsync((ApplicationUser?)null); // Usuário não encontrado

            // Act
            var result = await _controller.CreateProject(projectRequest);

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
            var result = await _controller.CreateProject(projectRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Verifica se o retorno é NotFound
        }
    }
}
