using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
using Newtonsoft.Json;
using X.PagedList;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class GetProjectsUnitTests 
    {
        private readonly IPagedList<Project>? _projects;


        [Fact]
        public async Task GetProjectsPagination_WithValidProjectList_ShouldReturnOkResult()
        {
            // Arrange
            var _mockRepo = new Mock<IProjectRepository>();
            var _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                 Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var _mockUserRepository = new Mock<IProject_UserRepository>();
            var _mockUserApplicationRepo = new Mock<IUserApplicationRepository>();
            var _mockCategoryRepo = new Mock<IProjectCategoryRepository>();
            var _mockResponsibleRepo = new Mock<IProjectResponsibleRepository>();

        var controller = new ProjectController(_mockRepo.Object, _mockUserManager.Object, _mockUserRepository.Object,
                                                    _mockUserApplicationRepo.Object, _mockCategoryRepo.Object, _mockResponsibleRepo.Object);
                
            var parametersPagination = new ProjectsParameters { PageNumber = 1, PageSize = 2 };
            string category = "Integração";
            string status = "Completed";
            string parameter = "MTFU";
            _mockRepo.Setup(repo => repo.GetProjectPaginationAsync(It.IsAny<ProjectsParameters>(),parameter, category, status)).ReturnsAsync(new StaticPagedList<Project>(
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
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            // Act
            var result = await controller.GetProjectsPagination(parametersPagination, parameter, category,status);

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
            var _mockRepo = new Mock<IProjectRepository>();
            var _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                 Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var _mockUserRepository = new Mock<IProject_UserRepository>();
            var _mockUserApplicationRepo = new Mock<IUserApplicationRepository>();
            var _mockCategoryRepo = new Mock<IProjectCategoryRepository>();
            var _mockResponsibleRepo = new Mock<IProjectResponsibleRepository>();
            var controller = new ProjectController(_mockRepo.Object, _mockUserManager.Object, _mockUserRepository.Object,
                                                        _mockUserApplicationRepo.Object, _mockCategoryRepo.Object, _mockResponsibleRepo.Object);

            var parametersPagination = new ProjectsParameters { PageNumber = 1, PageSize = 2 };
            string category = "Integração";
            string status = "Completed";
            string parameter = "MTFU";

            _mockRepo.Setup(repo => repo.GetProjectPaginationAsync(It.IsAny<ProjectsParameters>(), parameter, category, status)).ReturnsAsync(new StaticPagedList<Project>(
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
            var result = await controller.GetProjectsPagination(parametersPagination,parameter, category, status);

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
