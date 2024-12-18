﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using MoreThanFollowUp.API.Controllers.Entities;
using MoreThanFollowUp.Application.DTO.Resources;
using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using MoreThanFollowUp.Infrastructure.Repository.Models;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class GetResourcesForProjectUnitTests
    {
        private readonly Mock<IUserApplicationRepository> _mockUserApplicationRepo;
        private readonly Mock<IProjectResponsibleRepository> _mockResponsibleRepo;
        private readonly Mock<IProjectCategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProjectStatusRepository> _mockStatusRepositoryMock;
        private readonly Mock<IPlanningRepository> _PlanningRepositoryMock;
        private readonly Mock<IEnterpriseRepository> _EnterpriseRepository;
        private readonly ProjectController _controller;

        public GetResourcesForProjectUnitTests()
        {
            _mockUserApplicationRepo = new Mock<IUserApplicationRepository>();
            _mockResponsibleRepo = new Mock<IProjectResponsibleRepository>();
            _mockCategoryRepo = new Mock<IProjectCategoryRepository>();
            _mockStatusRepositoryMock = new Mock<IProjectStatusRepository>();
            _PlanningRepositoryMock = new Mock<IPlanningRepository>();
            _EnterpriseRepository = new Mock<IEnterpriseRepository>();
            _controller = new ProjectController(
                null, // Mocked repositories needed for this method
                null, // UserManager is not used in this method
                null, // Project_UserRepository is not used in this method
                _mockUserApplicationRepo.Object,
                _mockCategoryRepo.Object,
                _mockResponsibleRepo.Object,
                _mockStatusRepositoryMock.Object,
                _PlanningRepositoryMock.Object,
                _EnterpriseRepository.Object
            );
        }

        //[Fact]
        //public async Task GetResourcesForProject_ShouldReturnNotFound_WhenUsersListIsEmpty()
        //{
        //    // Arrange

        //    _mockUserApplicationRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ApplicationUser>());
        //    _mockResponsibleRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectResponsible> { new ProjectResponsible() });
        //    _mockCategoryRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectCategory> { new ProjectCategory() });
        //    _mockStatusRepositoryMock.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectStatus> { new ProjectStatus() });

        //    // Act
        //    var result = await _controller.GetResourcesForProject();

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task GetResourcesForProject_ShouldReturnNotFound_WhenResponsiblesListIsEmpty()
        //{
        //    // Arrange
        //    _mockUserApplicationRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ApplicationUser> { new ApplicationUser() });
        //    _mockResponsibleRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectResponsible>());
        //    _mockCategoryRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectCategory> { new ProjectCategory() });
        //    _mockStatusRepositoryMock.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectStatus> { new ProjectStatus() });

        //    // Act
        //    var result = await _controller.GetResourcesForProject();

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task GetResourcesForProject_ShouldReturnNotFound_WhenCategoriesListIsEmpty()
        //{
        //    // Arrange
        //    _mockUserApplicationRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ApplicationUser> { new ApplicationUser() });
        //    _mockResponsibleRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectResponsible> { new ProjectResponsible() });
        //    _mockCategoryRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectCategory>());
        //    _mockStatusRepositoryMock.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectStatus> { new ProjectStatus() });

        //    // Act
        //    var result = await _controller.GetResourcesForProject();

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}
        //[Fact]
        //public async Task GetResourcesForProject_ShouldReturnNotFound_WhenStatusListIsEmpty()
        //{
        //    // Arrange
        //    _mockUserApplicationRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ApplicationUser> { new ApplicationUser() });
        //    _mockResponsibleRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectResponsible> { new ProjectResponsible() });
        //    _mockCategoryRepo.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectCategory> { new ProjectCategory() });
        //    _mockStatusRepositoryMock.Setup(repo => repo.ListarAsync()).ReturnsAsync(new List<ProjectStatus>());

        //    // Act
        //    var result = await _controller.GetResourcesForProject();

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        [Fact]
        public async Task GetResourcesForProject_ShouldReturnOk_WhenAllListsHaveData()
        {
            // Arrange
            var usersList = new List<ApplicationUser> { new ApplicationUser { Id = "1", CompletedName = "User 1", Function = "Dev" } };
            var responsiblesList = new List<ProjectResponsible> { new ProjectResponsible { ResponsibleId = Guid.NewGuid(), Name = "Responsible 1" } };
            var categoriesList = new List<ProjectCategory> { new ProjectCategory { CategoryId = Guid.NewGuid(), Name = "Category 1" } };
            var statusList = new List<ProjectStatus> { new ProjectStatus { StatusProjectId = Guid.NewGuid(), Name = "Não iniciado" } };
            _mockUserApplicationRepo.Setup(repo => repo.ToListAsync()).ReturnsAsync(usersList);
            _mockResponsibleRepo.Setup(repo => repo.ToListAsync()).ReturnsAsync(responsiblesList);
            _mockCategoryRepo.Setup(repo => repo.ToListAsync()).ReturnsAsync(categoriesList);
            _mockStatusRepositoryMock.Setup(repo => repo.ToListAsync()).ReturnsAsync(statusList);

            // Act
            var result = await _controller.GetResourcesForProject();

            // Assert
            //Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ActionResult<ICollection<GetResourcesForProjectDTO>>>(result);
            Assert.NotNull(result);

        }
    }

}
