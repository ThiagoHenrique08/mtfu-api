using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoreThanFollowUp.API.Controllers.Entities;
using MoreThanFollowUp.Application.DTO.Project_DTO;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class PostProjectUnitTests 
    {

        //[Fact]
        //public async Task PostProject_WithValidProjectAndUser_ShouldReturnOkResult()
        //{
        //    //Arrange
        //    var _mockrepo = new Mock<IProjectRepository>();
        //    var _mockUserRepo = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>());
        //    var _mockProjectUser = new Mock<IProject_UserRepository>();
        //    var controller = new ProjectController(_mockrepo.Object, _mockUserRepo.Object, _mockProjectUser.Object);

        //    var project = new POSTProjectDTO{
              
        //        Title = "teste",
        //        Description = "teste",
        //        Category = "teste",
        //        Responsible = "teste",
        //        CreateDate = DateTime.Now,
           
        //    };
        //    var Users = new List<POSTUserToProjectDTO> {
        //    new POSTUserToProjectDTO {UserName="thiagsilva"},
        //    new POSTUserToProjectDTO {UserName="raphael"}
        //    };

        //   var requestDTO = new RequestProjectDTO { Project = project, UsersList = Users };

        //    //_mockrepo.Setup(repo => repo.CadastrarEmMassaAsync(It.IsAny<List<Project_User>>()));

        //}
        //[Fact]
        //public async Task PostProject_Return_BadRequest()
        //{

        //}
    }
}
