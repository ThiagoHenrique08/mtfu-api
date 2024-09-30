using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Project.Planning;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;

namespace MoreThanFollowUp.API.Controllers.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly IPlanningRepository _planningRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISprintRepository _sprintRepository;

        public PlanningController(IPlanningRepository planningRepository, IProjectRepository projectRepository, ISprintRepository sprintRepository)
        {
            _planningRepository = planningRepository;
            _projectRepository = projectRepository;
            _sprintRepository = sprintRepository;
        }

        [HttpGet]
        [Route("getPlanning")]
        public async Task<ActionResult<GETPlanningDTO>> GetPlanning(int ProjectId)
        {
            var planning = await _planningRepository.RecoverBy(p => p.ProjectId == ProjectId);


            if (planning is null)
            {
                return NotFound();
            }
            var sprints = _sprintRepository.SearchForAsync(s => s.PlanningId == planning.PlanningId);

            var planningDTO = new GETPlanningDTO
            {
                PlanningDTO = new PlanningDTO
                {
                    PlanningId = planning.PlanningId,
                    DocumentationLink = planning.DocumentationLink,
                    PlanningDescription = planning.PlanningDescription
                },

                TotalSprint = sprints.Count()


            };

            return Ok(planningDTO);

        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<POSTPlanningDTO>> CreatePlanning([FromBody] POSTPlanningDTO planningDTO)
        {
            if (planningDTO is null) { return NotFound(); }

            var project = await _projectRepository.RecoverBy(p => p.ProjectId == planningDTO.ProjectId);

            var newPlanning = new Planning()
            {
                DocumentationLink = planningDTO.DocumentationLink,
                PlanningDescription = planningDTO.PlanningDescription,
                ProjectId = planningDTO.ProjectId,
                Project = project

            };
            await _planningRepository.RegisterAsync(newPlanning);

            return Ok(newPlanning);
        }

        [HttpPatch]
        [Route("upate")]
        public async Task<ActionResult<PATCHPlanningDTO>> UpdatePlanning([FromBody] PATCHPlanningDTO planningDTO)
        {
            if (planningDTO is null) { return NotFound(); }

            var planning = await _planningRepository.RecoverBy(p => p.ProjectId == planningDTO.PlanningId);

            if (planning is null) { return NotFound(); }

            planning!.DocumentationLink = planningDTO.DocumentationLink ?? planning!.DocumentationLink;
            planning.PlanningDescription = planningDTO.PlanningDescription ?? planning.PlanningDescription;

            await _planningRepository.UpdateAsync(planning);

            return Ok(planning);
        }


    }
}
