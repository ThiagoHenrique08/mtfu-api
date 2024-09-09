using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Project_DTO;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
using MoreThanFollowUp.Infrastructure.Repository.Projects;
using Newtonsoft.Json;
using X.PagedList;

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
        //[Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
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
                var ProjectExist = await _projectRepository.RecuperarPorAsync(p => p.Title!.ToUpper().Equals(newProject.Title!.ToUpper()));

                if (ProjectExist is null)
                {
                    await _projectRepository.AdicionarAsync(newProject);
                }

                var newProjectCadastrado = await _projectRepository.RecuperarPorAsync(p => p.Title!.ToUpper().Equals(newProject.Title!.ToUpper()));
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

        [HttpPost]
        [Route("addUserToProject/{IdProject:int}")]
        //[Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
        public async Task<ActionResult> PosUserToProject([FromRoute] int IdProject, [FromBody] List<POSTUserToProjectDTO> users)
        {
            try
            {

                var ProjectExist = await _projectRepository.RecuperarPorAsync(p => p.ProjectId.Equals(IdProject));
                if (ProjectExist is null)
                {
                    return NotFound();
                }
                var newListProjectUser = new List<Project_User>();

                foreach (var user in users)
                {
                    var result = await _userManager.FindByNameAsync(user.UserName!);

                    if (result != null)
                    {
                        newListProjectUser.Add(new Project_User
                        {
                            Project = ProjectExist,
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
        public async Task<ActionResult<IEnumerable<GETProjectDTO>>> GetProjectsPagination([FromQuery] ProjectsParameters projectsParameters)
        {
            try
            {
                var projects = await _projectRepository.GetProjectPaginationAsync(projectsParameters);

                return GetProjects(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private ActionResult<IEnumerable<GETProjectDTO>> GetProjects(IPagedList<Project> projects)
        {


            var newListProject = new List<GETProjectDTO>();
            var usersList = new List<POSTUserToProjectDTO>();
            //var users = _project_UserRepository.RecuperarPorAsync(p => p.ProjectId == project.ProjectId);
            foreach (var project in projects)
            {

                newListProject.Add(new GETProjectDTO
                {
                    ProjectId = project.ProjectId,
                    Title = project.Title,
                    Responsible = project.Responsible,
                    Category = project.Category,
                    Description = project.Description,
                    EndDate = DateTime.Now,
                    Projects_Users = project.Projects_Users!.Select(p => p.User!.CompletedName).ToList()!

                });
            }

            var metadata = new
            {
                projects.Count,
                projects.PageSize,
                projects.PageCount,
                projects.TotalItemCount,
                projects.HasNextPage,
                projects.HasPreviousPage
            };


            //Cria um cabeçalho HTTP personalizado para ser mostrar na resposta:
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(newListProject);
        }


        [HttpPatch]
        [Route("changeProject")]
        public async Task<ActionResult> PatchProject([FromBody] PATCHProjectDTO projectDTO)
        {
            try
            {

                if (projectDTO is null)
                {
                    return NotFound();
                }
                var projectExist = await _projectRepository.RecuperarPorAsync(p => p.ProjectId == projectDTO.ProjectId);

                if (projectExist is not null)
                {

                    projectExist.Title = projectDTO.Title ?? projectExist.Title;
                    projectExist.Responsible = projectDTO.Responsible ?? projectExist.Responsible;
                    projectExist.Category = projectDTO.Category ?? projectExist.Category;
                    projectExist.Description = projectDTO.Description ?? projectExist.Description;
                    projectExist.CreateDate = projectDTO.CreateDate ?? projectExist.CreateDate;
                }
                await _projectRepository.AtualizarAsync(projectExist!);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete(int idProject)
        {
            try
            {
                var project = await _projectRepository.RecuperarPorAsync(p => p.ProjectId == idProject);

                if (project is null) { return NotFound(); }

                await _projectRepository.DeletarAsync(project);

                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

    }

}
