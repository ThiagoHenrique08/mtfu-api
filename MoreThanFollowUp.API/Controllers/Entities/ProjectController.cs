using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Application.DTO.Project_DTO;
using MoreThanFollowUp.Application.DTO.Resources;
using MoreThanFollowUp.Application.DTO.Users;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using MoreThanFollowUp.Infrastructure.Pagination;
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
        private readonly IUserApplicationRepository _userApplicationRepository;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly IProjectResponsibleRepository _projectResponsibleRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;

        public ProjectController(IProjectRepository projectRepository, UserManager<ApplicationUser> userManager, IProject_UserRepository project_UserRepository, IUserApplicationRepository userApplicationRepository, IProjectCategoryRepository projectCategoryRepository, IProjectResponsibleRepository projectResponsibleRepository, IProjectStatusRepository projectStatusRepository)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _project_UserRepository = project_UserRepository;
            _userApplicationRepository = userApplicationRepository;
            _projectCategoryRepository = projectCategoryRepository;
            _projectResponsibleRepository = projectResponsibleRepository;
            _projectStatusRepository = projectStatusRepository;
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
                    var result = await _userApplicationRepository.RecuperarPorAsync(p => p.CompletedName!.ToUpper().Equals(user.CompletedName));//_userManager.FindByNameAsync(user.UserName!);

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
                    var result = await _userApplicationRepository.RecuperarPorAsync(p => p.CompletedName.ToUpper().Equals(user.CompletedName));//_userManager.FindByNameAsync(user.com!);

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
        public async Task<ActionResult<IEnumerable<GETProjectDTO>>> GetProjectsPagination([FromQuery] ProjectsParameters projectsParametersPagination, string? parameter, string? category, string? status)
        {
            try
            {
                var projects = await _projectRepository.GetProjectPaginationAsync(projectsParametersPagination, parameter!, category!, status!);

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
                    Status = project.Status,
                    Description = project.Description,
                    CreateDate = project.CreateDate,
                    EndDate = project.EndDate,
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
                else
                {
                    return NotFound();
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
                //Deleta primeiro todos os relacionamentos de Projeto com usuário antes de Deletar o Projeto

                var list = _project_UserRepository.SearchForAsync(p => p.ProjectId == idProject);

                if (list is not null)
                {
                    await _project_UserRepository.RemoveRange(list);
                }

                // Deleta o Projeto de Fato
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

        [HttpGet]
        [Route("getResourcesForProject")]
        public async Task<ActionResult<ICollection<GetResourcesForProjectDTO>>> GetResourcesForProject()
        {
            var users = await _userApplicationRepository.ListarAsync();
            var responsibles = await _projectResponsibleRepository.ListarAsync();
            var categories = await _projectCategoryRepository.ListarAsync();
            var projectStatus = await _projectStatusRepository.ListarAsync();
            if (users.IsNullOrEmpty() || responsibles.IsNullOrEmpty() || categories.IsNullOrEmpty() || projectStatus.IsNullOrEmpty())
            {
                return NotFound();
            }

            var resourcesForProject = new GetResourcesForProjectDTO();

            foreach (var user in users)
            {
                resourcesForProject.Users.Add(new GetUsersDTO { UserId = user.Id, NameCompleted = user.CompletedName, Function = user.Function });
            }
            foreach (var responsbile in responsibles)
            {
                resourcesForProject.Responsibles.Add(new GetResponsibleDTO { ResponsibleId = responsbile.ResponsibleId, Name = responsbile.Name });
            }
            foreach (var category in categories)
            {
                resourcesForProject.Categories.Add(new GetCategoryDTO { CategoryId = category.CategoryId, Name = category.Name });
            }
            foreach (var status in projectStatus)
            {
                resourcesForProject.StatusProjects.Add(new GETStatusDTO { StatusProjectId = status.StatusProjectId, Name = status.Name });
            }

            return Ok(resourcesForProject);

        }
    }

}
