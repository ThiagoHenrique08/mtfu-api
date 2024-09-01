using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Project_DTO;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
using Newtonsoft.Json;

namespace MoreThanFollowUp.API.Controllers.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProject_UserRepository _project_UserRepository;

        public ProjectController(IProjectRepository projectRepository, UserManager<ApplicationUser> userManager, IProject_UserRepository project_UserRepository)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _project_UserRepository = project_UserRepository;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
        public async Task<ActionResult> PostProject([FromBody] RequestProjectDTO projectRequest)
        {

            try
            {
                if (projectRequest.Project == null) { return NotFound(); }

                var newProject = new Project()
                {
                    Title = projectRequest.Project.Title,
                    Responsible = projectRequest.Project.Responsible,
                    Category = projectRequest.Project.Category,
                    Description = projectRequest.Project.Description,
                    CreateDate = DateTime.Now,
                };
                var ProjectExist = _projectRepository.RecuperarPorAsync(p => p.Title!.ToUpper().Equals(newProject.Title!.ToUpper()));

                if (ProjectExist is null)
                {
                    await _projectRepository.AdicionarAsync(newProject);
                }

                var newProjectCadastrado = _projectRepository.RecuperarPorAsync(p => p.Title!.ToUpper().Equals(newProject.Title!.ToUpper()));

                var newListProjectUser = new List<Project_User>();

                foreach (var user in projectRequest.UsersList!)
                {

                    var result = await _userManager.FindByNameAsync(user.UserName!);

                    if (result != null)
                    {
                        newListProjectUser.Add(new Project_User
                        {
                            Project = newProjectCadastrado,
                            User = result,
                            CreateDate = DateTime.Now

                        });
                    }
                    else
                    {

                        return NotFound("User not exist!");

                    }


                }
                await _project_UserRepository.CadastrarEmMassaAsync(newListProjectUser);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("pagination")]
        public ActionResult<IEnumerable<GETProject>> GetProjectsPagination([FromQuery] ProjectsParameters projectsParameters)
        {
            var projects = _projectRepository.GetProjectPagination(projectsParameters);

            if (projects is null) { return NotFound(); }

            var metadata = new
            {
                projects.TotalCount,
                projects.PageSize,
                projects.CurrentPage,
                projects.TotalPages,
                projects.HasNext,
                projects.HasPrevious
            };

            //Cria um cabeçalho HTTP personalizado para ser mostrar na resposta:
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var newListProject = new List<GETProject>();


            foreach (var project in projects)
            {
                newListProject.Add(new GETProject
                {
                    ProjectId = project.ProjectId,
                    Title = project.Title,
                    Responsible = project.Responsible,
                    Category = project.Category,
                    Description = project.Description,
                    CreateDate = DateTime.Now,

                });
            }
            return Ok(newListProject);

        }

        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<GETProject>>> GetProject()
        {
            var newListProject = new List<GETProject>();

            var projects = await _projectRepository.ListarAsync();

            if (projects is null) { return NotFound(); }

            foreach (var project in projects)
            {
                newListProject.Add(new GETProject
                {
                    ProjectId = project.ProjectId,
                    Title = project.Title,
                    Responsible = project.Responsible,
                    Category = project.Category,
                    Description = project.Description,
                    CreateDate = DateTime.Now,

                });
            }
            return Ok(newListProject);
        }
    }
}