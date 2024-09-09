using MoreThanFollowUp.API.Controllers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Tests.UnitTests.Projects
{
    public class PatchProjectsUnitTests : IClassFixture<ProjectsUnitTestsController>
    {

        private readonly ProjectController _controller;

        //public PatchProjectsUnitTests(ProjectsUnitTestsController controller)
        //{
        //    _controller = new ProjectController(controller._repository, controller._userManager, controller._project_UserRepository);
        //}

        [Fact]
        public async Task PatchProject_Update_Return_Ok()
        {

        }
        [Fact]
        public async Task PatchProject_Update_Return_BadRequest()
        {

        }

    }
}
