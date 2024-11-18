using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Project;
using MoreThanFollowUp.Application.DTO.Resources;
using MoreThanFollowUp.Application.DTO.Users;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
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
        private readonly IPlanningRepository _planningRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;

        public ProjectController(IProjectRepository projectRepository, UserManager<ApplicationUser> userManager, IProject_UserRepository project_UserRepository, IUserApplicationRepository userApplicationRepository, IProjectCategoryRepository projectCategoryRepository, IProjectResponsibleRepository projectResponsibleRepository, IProjectStatusRepository projectStatusRepository, IPlanningRepository planningRepository, IEnterpriseRepository enterpriseRepository)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _project_UserRepository = project_UserRepository;
            _userApplicationRepository = userApplicationRepository;
            _projectCategoryRepository = projectCategoryRepository;
            _projectResponsibleRepository = projectResponsibleRepository;
            _projectStatusRepository = projectStatusRepository;
            _planningRepository = planningRepository;
            _enterpriseRepository = enterpriseRepository;
        }


        [HttpPost]
        [Route("create")]
        //[Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
        public async Task<ActionResult> CreateProject([FromBody] RequestProjectDTO projectRequest)
        {
            try
            {
                if (projectRequest.Project == null) { return NotFound(); }

                var enterprise = await _enterpriseRepository.RecoverBy(p => p.EnterpriseId == projectRequest.Project.EnterpriseId);


                var newProject = new Project()
                {
                    Title = projectRequest.Project.Title,
                    Responsible = projectRequest.Project.Responsible,
                    Category = projectRequest.Project.Category,
                    Description = projectRequest.Project.Description,
                    CreateDate = DateTime.Now,
                    EnterpriseId = projectRequest.Project.EnterpriseId,
                    Enterprise = enterprise
                };
                var ProjectExist = await _projectRepository.RecoverBy(p => p.Title!.ToUpper().Equals(newProject.Title!.ToUpper()));

                if (ProjectExist is null)
                {
                    await _projectRepository.RegisterAsync(newProject);
                }

                var newProjectCadastrado = await _projectRepository.RecoverBy(p => p.Title!.ToUpper().Equals(newProject.Title!.ToUpper()));
                var newListProjectUser = new List<Project_User>();

                foreach (var user in projectRequest.UsersList!)
                {
                    var result = await _userApplicationRepository.RecoverBy(p => p.Id == user.UserId);//_userManager.FindByNameAsync(user.UserName!);

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
                await _project_UserRepository.RegisterList(newListProjectUser);
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
        public async Task<ActionResult> PostUserToProject([FromRoute] int IdProject, [FromBody] List<POSTUserToProjectDTO> users)
        {
            try
            {
                var ProjectExist = await _projectRepository.RecoverBy(p => p.ProjectId.Equals(IdProject));
                if (ProjectExist is null)
                {
                    return NotFound();
                }
                var newListProjectUser = new List<Project_User>();

                foreach (var user in users)
                {
                    var result = await _userApplicationRepository.RecoverBy(p => p.Id == user.UserId);//_userManager.FindByNameAsync(user.com!);

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
                await _project_UserRepository.RegisterList(newListProjectUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<IEnumerable<GETProjectDTO>>> GetProjectWithPagination([FromQuery] ProjectsParameters projectsParametersPagination, string? parameter, string? category, string? status,Guid enterpriseId)
        {
            try
            {
                var projects = await _projectRepository.GetProjectPaginationAsync(projectsParametersPagination, parameter!, category!, status!, enterpriseId);

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
            // var usersList = new List<POSTUserToProjectDTO>();
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
        [Route("update")]
        public async Task<ActionResult> UpdateProject([FromBody] PATCHProjectDTO projectDTO)
        {
            try
            {

                if (projectDTO is null)
                {
                    return NotFound();
                }
                var projectExist = await _projectRepository.RecoverBy(p => p.ProjectId == projectDTO.ProjectId);

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
                await _projectRepository.UpdateAsync(projectExist!);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteProject(Guid idProject)
        {
            try
            {
                //Deleta primeiro todos os relacionamentos de Projeto com usuário antes de Deletar o Projeto

                var list = _project_UserRepository.SearchForAsync(p => p.ProjectId == idProject);

                if (list is not null)
                {
                    await _project_UserRepository.RemoveRange(list);
                }

                var planningList = _planningRepository.SearchForAsync(p => p.ProjectId == idProject);

                if (list is not null)
                {
                    await _project_UserRepository.RemoveRange(list);
                }

                // Deleta o Projeto de Fato
                var project = await _projectRepository.RecoverBy(p => p.ProjectId == idProject);

                if (project is null) { return NotFound(); }

                await _projectRepository.DeleteAsync(project);

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
            var users = await _userApplicationRepository.ToListAsync();
            var responsibles = await _projectResponsibleRepository.ToListAsync();
            var categories = await _projectCategoryRepository.ToListAsync();
            var projectStatus = await _projectStatusRepository.ToListAsync();

            //if (users.IsNullOrEmpty() || responsibles.IsNullOrEmpty() || categories.IsNullOrEmpty() || projectStatus.IsNullOrEmpty())
            //{
            //    return NotFound();
            //}

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


        [HttpPost]
        [Route("addManyUserToManyProjects")]
        //[Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
        public async Task<ActionResult> PostManyUserToManyProjects([FromBody] List<POSTUserToProjectDTO> users)
        {
            try
            {

                var listProjects = await _projectRepository.ToListAsync();


                if (listProjects is null)
                {
                    return NotFound();
                }

                var newListProjectUser = new List<Project_User>();

                foreach (var project in listProjects)
                {

                    foreach (var user in users)
                    {
                        var result = await _userApplicationRepository.RecoverBy(p => p.Id == user.UserId);//_userManager.FindByNameAsync(user.com!);

                        if (result != null)
                        {
                            newListProjectUser.Add(new Project_User
                            {
                                Project = project,
                                User = result,
                                CreateDate = DateTime.Now
                            });
                        }
                        else
                        {
                            return NotFound("User not exist!");
                        }
                        
                    }
                }
                await _project_UserRepository.RegisterList(newListProjectUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
