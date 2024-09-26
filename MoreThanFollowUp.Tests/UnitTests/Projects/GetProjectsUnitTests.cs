using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
using MoreThanFollowUp.Infrastructure.Pagination;
using Newtonsoft.Json;
using X.PagedList;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class GetProjectsUnitTests 
    {
        private readonly IPagedList<Project>? _projects;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IProject_UserRepository> _projectUserRepositoryMock;
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepo;
        private readonly Mock<IProjectCategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProjectResponsibleRepository> _mockResponsibleRepo;
        private readonly Mock<IProjectStatusRepository> _mockStatusRepositoryMock;
        private readonly ProjectController _controller;

        public GetProjectsUnitTests()
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

        [Fact]
        public async Task GetProjectsPagination_WithValidProjectList_ShouldReturnOkResult()
        {
            // Arrange
                
            var parametersPagination = new ProjectsParameters { PageNumber = 1, PageSize = 2 };
            string category = "Integração";
            string status = "Completed";
            string parameter = "MTFU";
            _projectRepositoryMock.Setup(repo => repo.GetProjectPaginationAsync(It.IsAny<ProjectsParameters>(),parameter, category, status)).ReturnsAsync(new StaticPagedList<Project>(
            new List<Project>
            {
                new Project { ProjectId = 1, Title = "Project 1", Category = "Backend", Description = "Description 1", Responsible = "John", CreateDate = DateTime.Now, EndDate = DateTime.Now, Projects_Users = new List<Project_User>() },
                new Project { ProjectId = 2, Title = "Project 2", Category = "Frontend", Description = "Description 2", Responsible = "Doe", CreateDate = DateTime.Now, EndDate = DateTime.Now, Projects_Users = new List<Project_User>() }
            },
            1, // Página atual
            2, // Tamanho da página
            2 // Total de itens
        ));

            // Criando o HttpContext manualmente para simular os headers
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            // Act
            var result = await _controller.GetProjectWithPagination(parametersPagination, parameter, category,status);

            // Assert

            // Verificando se o resultado não é nulo e se o tipo de retorno é o esperado
            result.Should().NotBeNull();
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IEnumerable<GETProjectDTO>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);

            // Verificando se o resultado da ação é OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Verificando os metadados de paginação no cabeçalho HTTP
            var paginationMetadata = httpContext.Response.Headers["X-Pagination"];
            paginationMetadata.Should().NotBeNull();

            // Desserializando os metadados para verificar o conteúdo
            var metadata = JsonConvert.DeserializeObject<dynamic>(paginationMetadata.ToString());
            Assert.Equal(2, (int)metadata!.TotalItemCount); // Verifica se o total de itens está correto
            Assert.Equal(1, (int)metadata.PageCount);      // Verifica se o número de páginas está correto
            Assert.Equal(2, (int)metadata.PageSize);       // Verifica o tamanho da página
            Assert.Equal(2, (int)metadata.Count);          // Verifica o número de itens retornados na página
        }


        [Fact]
        public async Task GetProjectsPagination_InvalidReturn_ShouldBadRequestResult()
        {
            // Arrange
            var parametersPagination = new ProjectsParameters { PageNumber = 1, PageSize = 2 };
            string category = "Integração";
            string status = "Completed";
            string parameter = "MTFU";

            _projectRepositoryMock.Setup(repo => repo.GetProjectPaginationAsync(It.IsAny<ProjectsParameters>(), parameter, category, status)).ReturnsAsync(new StaticPagedList<Project>(
            new List<Project>
            {
                new Project { ProjectId = 1, Title = "Project 1", Category = "Backend", Description = "Description 1", Responsible = "John", CreateDate = DateTime.Now, EndDate = DateTime.Now, Projects_Users = new List<Project_User>() },
                new Project { ProjectId = 2, Title = "Project 2", Category = "Frontend", Description = "Description 2", Responsible = "Doe", CreateDate = DateTime.Now, EndDate = DateTime.Now, Projects_Users = new List<Project_User>() }
            },
            1, // Página atual
            2, // Tamanho da página
            2 // Total de itens
        ));

            // Act
            var result = await _controller.GetProjectWithPagination(parametersPagination,parameter, category, status);

            // Assert

            // Verificando se o resultado não é nulo e se o tipo de retorno é o esperado
            result.Should().NotBeNull();
            Assert.NotNull(result);
            Assert.IsType<ActionResult<IEnumerable<GETProjectDTO>>>(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);

            // Verificando se o resultado da ação é BadRequestObjectResult
            var okResult = Assert.IsType<BadRequestObjectResult>(result.Result);

        }
    }
}
