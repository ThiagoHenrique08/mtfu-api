using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Project.RequirementAnalysis;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;

namespace MoreThanFollowUp.API.Controllers.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementAnalysisController : ControllerBase
    {

        private readonly IRequirementAnalysisRepository _requirementAnalysisRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISprintRepository _sprintRepository;
        public RequirementAnalysisController(IRequirementAnalysisRepository requirementAnalysisRepository, IProjectRepository projectRepository, ISprintRepository sprintRepository)
        {
            _requirementAnalysisRepository = requirementAnalysisRepository;
            _projectRepository = projectRepository;
            _sprintRepository = sprintRepository;
        }

        [HttpGet]
        [Route("getRequirementAnalysis")]
        public async Task<ActionResult<IEnumerable<GETRequirementAnalysisDTO>>> GetRequirementAnalysis(Guid ProjectId)
        {
            try
            {
                var requirementAnalysis = await _requirementAnalysisRepository.RecoverBy(p => p.ProjectId == ProjectId);

                if (requirementAnalysis is null) { return NotFound(); }

                var requirementAnalysisDTO = new GETRequirementAnalysisDTO
                {
                    RequirementAnalysisId = requirementAnalysis.RequirementAnalysisId,
                    StartDate = requirementAnalysis.StartDate,
                    EndDate = requirementAnalysis.EndDate,
                };

                return Ok(requirementAnalysisDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GETRequirementAnalysisDTO>> CreateRequirementAnalysis(POSTRequirementAnalysisDTO requirementAnalysisDTO)
        {
            try
            {
                if (requirementAnalysisDTO is null) { return NotFound(); }

                var project = await _projectRepository.RecoverBy(p => p.ProjectId == requirementAnalysisDTO.ProjectId);

                if (project is null) { return NotFound(); }

                var newRequirementAnalysis = new RequirementAnalysis
                {
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Project = project,
                    ProjectId = project.ProjectId,
                };

                await _requirementAnalysisRepository.RegisterAsync(newRequirementAnalysis);
                var idRequirementAnalysisCreated = newRequirementAnalysis.RequirementAnalysisId;
                //var projectResponse = await _projectRepository.RecoverBy(p => p.ProjectId == p.ProjectId);
                //var idPlanning = projectResponse!.Planning!.PlanningId;
                var getRequirementAnalysis = await _requirementAnalysisRepository.RecoverBy(s => s.RequirementAnalysisId == idRequirementAnalysisCreated);

                var getRequirementAnalysisDTO = new GETRequirementAnalysisDTO
                {
                    RequirementAnalysisId = getRequirementAnalysis!.RequirementAnalysisId,
                    StartDate = getRequirementAnalysis.StartDate,
                    EndDate = getRequirementAnalysis.StartDate,
                };

                return Ok(getRequirementAnalysisDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
        [HttpPatch]
        [Route("update")]
        public async Task<ActionResult<GETRequirementAnalysisDTO>> UpdatePlanning([FromBody] PATCHRequirementAnalysisDTO requirementAnalysisDTO)
        {
            try
            {
                if (requirementAnalysisDTO is null) { return NotFound(); }

                var requirementAnalysis = await _requirementAnalysisRepository.RecoverBy(p => p.RequirementAnalysisId == requirementAnalysisDTO.RequirementAnalysisId);

                if (requirementAnalysis is null) { return NotFound(); }

                requirementAnalysis!.EndDate = requirementAnalysisDTO.EndDate ?? requirementAnalysis!.EndDate;

                await _requirementAnalysisRepository.UpdateAsync(requirementAnalysis);

                var getrequirementAnalysis = await _requirementAnalysisRepository.RecoverBy(s => s.RequirementAnalysisId == requirementAnalysis.RequirementAnalysisId);

                var getRequirementAnalysisDTO = new GETRequirementAnalysisDTO
                {
                    RequirementAnalysisId = getrequirementAnalysis!.RequirementAnalysisId,
                    StartDate = getrequirementAnalysis.StartDate,
                    EndDate = getrequirementAnalysis.StartDate,
                };
                return Ok(getRequirementAnalysisDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
