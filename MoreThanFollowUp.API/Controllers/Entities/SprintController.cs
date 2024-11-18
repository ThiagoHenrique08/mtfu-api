using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Application.DTO.Project;
using MoreThanFollowUp.Application.DTO.Project.Sprint;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using MoreThanFollowUp.Infrastructure.Repository.Entities.Projects;

namespace MoreThanFollowUp.API.Controllers.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IPlanningRepository _planningRepository;
        private readonly IUserApplicationRepository _userApplicationRepository;
        private readonly ISprint_UserRepository _sprintUserRepository;
        private readonly IRequirementAnalysisRepository _requirementAnalysis;
        public SprintController(ISprintRepository sprintRepository, IPlanningRepository planningRepository, IUserApplicationRepository userApplicationRepository, ISprint_UserRepository sprintUserRepository)
        {
            _sprintRepository = sprintRepository;
            _planningRepository = planningRepository;
            _userApplicationRepository = userApplicationRepository;
            _sprintUserRepository = sprintUserRepository;
        }

        [HttpGet]
        [Route("getSprint")]
        [OutputCache(Duration = 400)]
        public async Task<ActionResult<IEnumerable<GETSprintDTO>>> GetSprintPlanning(Guid? PhaseId)
        {

            try
            {
                var sprintPlannings = _sprintRepository.SearchForAsync(p => p.PlanningId == PhaseId);
                var sprintRequirementAnalysis = _sprintRepository.SearchForAsync(p => p.RequirementAnalysisId == PhaseId);

                if (!sprintPlannings.IsNullOrEmpty())
                {
                    var sprintDTO = new List<GETSprintDTO>();
                    foreach (var sprint in sprintPlannings)
                    {
                        sprintDTO.Add(new GETSprintDTO
                        {
                            SprintId = sprint.SprintId,
                            Title = sprint.Title,
                            Description = sprint.Description,
                            StartDate = sprint.EndDate,
                            EndDate = sprint.EndDate,
                            Status = sprint.Status,
                            Sprint_Users = sprint.Sprint_Users!.Select(p => p.User!.CompletedName).ToList()!
                        });
                    }
                    return Ok(sprintDTO);
                }
                if (!sprintRequirementAnalysis.IsNullOrEmpty())
                {
                    var sprintDTO = new List<GETSprintDTO>();
                    foreach (var sprint in sprintRequirementAnalysis)
                    {
                        sprintDTO.Add(new GETSprintDTO
                        {
                            SprintId = sprint.SprintId,
                            Title = sprint.Title,
                            Description = sprint.Description,
                            StartDate = sprint.EndDate,
                            EndDate = sprint.EndDate,
                            Status = sprint.Status,
                            Sprint_Users = sprint.Sprint_Users!.Select(p => p.User!.CompletedName).ToList()!
                        });
                    }
                    return Ok(sprintDTO);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateSprintPlanning([FromBody] RequestSprintDTO sprintRequest)
        {

            try
            {

                var planning = await _planningRepository.RecoverBy(p => p.PlanningId == sprintRequest.Sprint!.PhaseId);
                var RequirementAnalysis = await _requirementAnalysis.RecoverBy(p => p.RequirementAnalysisId == sprintRequest.Sprint!.PhaseId);
                Guid SprintIdCreated = Guid.Empty;

                if (planning is not null)
                {
                    var newSprint = new Sprint()
                    {
                        Title = sprintRequest.Sprint.Title,
                        Description = sprintRequest.Sprint.Description,
                        Status = sprintRequest.Sprint.Status,
                        StartDate = DateTime.Now,
                        EndDate = null,
                        SprintScore = sprintRequest.Sprint.SprintScore,
                        PlanningId = planning!.PlanningId,
                        Planning = planning
                    };
                    await _sprintRepository.RegisterAsync(newSprint);
                    SprintIdCreated = newSprint.SprintId;
                }
                else if (RequirementAnalysis is not null)
                {
                    var newSprint = new Sprint()
                    {
                        Title = sprintRequest.Sprint!.Title,
                        Description = sprintRequest.Sprint.Description,
                        Status = sprintRequest.Sprint.Status,
                        StartDate = DateTime.Now,
                        EndDate = null,
                        SprintScore = sprintRequest.Sprint.SprintScore,
                        RequirementAnalysisId = RequirementAnalysis!.RequirementAnalysisId,
                        RequirementAnalysis = RequirementAnalysis
                    };
                    await _sprintRepository.RegisterAsync(newSprint);
                    SprintIdCreated = newSprint.SprintId;
                }

                var newSprintRegistred = await _sprintRepository.RecoverBy(p => p.SprintId == SprintIdCreated);
                var newListSprintUser = new List<Sprint_User>();

                foreach (var user in sprintRequest.UsersList!)
                {
                    var result = await _userApplicationRepository.RecoverBy(p => p.Id == user.UserId);//_userManager.FindByNameAsync(user.UserName!);

                    if (result != null)
                    {
                        newListSprintUser.Add(new Sprint_User
                        {
                            Sprint = newSprintRegistred,
                            User = result,
                            CreateDate = DateTime.Now
                        });
                    }
                    else
                    {
                        return NotFound("User not exist!");
                    }
                }
                await _sprintUserRepository.RegisterList(newListSprintUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("addUserToSprint/{IdSprint:int}")]
        //[Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
        public async Task<ActionResult> PostUserToSprint([FromRoute] Guid IdSprint, [FromBody] List<POSTUserToSprintDTO> users)
        {
            try
            {
                var SprintExist = await _sprintRepository.RecoverBy(p => p.SprintId == IdSprint);
                if (SprintExist is null)
                {
                    return NotFound("Sprint not exist!");
                }
                var newListSprinttUser = new List<Sprint_User>();

                foreach (var user in users)
                {
                    var result = await _userApplicationRepository.RecoverBy(p => p.Id == user.UserId);//_userManager.FindByNameAsync(user.com!);

                    if (result != null)
                    {
                        newListSprinttUser.Add(new Sprint_User
                        {
                            Sprint = SprintExist,
                            User = result,
                            CreateDate = DateTime.Now
                        });
                    }
                    else
                    {
                        return NotFound("User not exist!");
                    }
                }
                await _sprintUserRepository.RegisterList(newListSprinttUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("update")]
        public async Task<ActionResult<GETSprintDTO>> UpdateSprint([FromBody] PATCHSprintDTO sprintDTO)
        {
            if (sprintDTO is null) { return NotFound(); }

            var sprint = await _sprintRepository.RecoverBy(p => p.SprintId == sprintDTO.SprintId);

            if (sprint is null) { return NotFound(); }

            sprint.Title = sprintDTO.Title ?? sprint.Title;
            sprint.Description = sprintDTO.Description ?? sprint.Description;
            sprint.Status = sprintDTO.Status ?? sprint.Status;
            sprint.SprintScore = sprint.SprintScore ?? sprint.SprintScore;
            sprint.EndDate = sprint.EndDate ?? sprint.EndDate;

            await _sprintRepository.UpdateAsync(sprint);

            var getSprint = await _sprintRepository.RecoverBy(s => s.SprintId == sprint.SprintId);

            var getSprintDTO = new GETSprintDTO
            {
                SprintId = getSprint!.SprintId,
                Title = getSprint.Title,
                Description = getSprint.Description,
                SprintScorte = getSprint.SprintScore,
                StartDate = getSprint.StartDate,
                EndDate = getSprint.EndDate,
                Status = getSprint.Status,
            };

            return Ok(getSprintDTO);
        }


        [HttpPost]
        [Route("addManyUserToManySprints")]
        //[Authorize(Policy = "AdminOnlyAndScrumMasterOnly")]
        public async Task<ActionResult> PostManyUserToManyProjects([FromBody] List<POSTUserToProjectDTO> users)
        {
            try
            {

                var listSprints = await _sprintRepository.ToListAsync();


                if (listSprints is null)
                {
                    return NotFound();
                }

                var newListSprintUser = new List<Sprint_User>();

                foreach (var sprint in listSprints)
                {

                    foreach (var user in users)
                    {
                        var result = await _userApplicationRepository.RecoverBy(p => p.Id == user.UserId);//_userManager.FindByNameAsync(user.com!);

                        if (result != null)
                        {
                            newListSprintUser.Add(new Sprint_User
                            {
                                Sprint = sprint,
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
                await _sprintUserRepository.RegisterList(newListSprintUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
