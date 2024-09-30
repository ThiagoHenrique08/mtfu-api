using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Project.Sprint;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;

namespace MoreThanFollowUp.API.Controllers.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IPlanningRepository _planningRepository;

        public SprintController(ISprintRepository sprintRepository, IPlanningRepository planningRepository)
        {
            _sprintRepository = sprintRepository;
            _planningRepository = planningRepository;
        }

        [HttpGet]
        [Route("getSprint")]
        public async Task<ActionResult<IEnumerable<GETSprintDTO>>> GetPlanning(int planningId)
        {
            var sprints =  _sprintRepository.SearchForAsync(p=>p.PlanningId == planningId);

            if (sprints is null)
            {
                return NotFound();
            }

            var sprintDTO = new List<GETSprintDTO>();

            foreach (var sprint in sprints)
            {
                sprintDTO.Add(new GETSprintDTO
                {
                    SprintId = sprint.SprintId,
                    Title = sprint.Title,
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Status = sprint.Status,
                });
            }

            return Ok(sprintDTO);

        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<POSTSprintDTO>> CreatePlanning([FromBody] POSTSprintDTO sprintDTO)
        {
            if (sprintDTO is null) { return NotFound(); }

            var planning = await _planningRepository.RecoverBy(p => p.PlanningId == sprintDTO.PlanningId);

            var newSprint = new Sprint()
            {
                Title = sprintDTO.Title,
                Description = sprintDTO.Description,
                StartDate = DateTime.Now,
                EndDate = null,
                Status = sprintDTO.Status,
                PlanningId = sprintDTO.PlanningId,
                Planning = planning
            };
            await _sprintRepository.RegisterAsync(newSprint);

            return Ok(newSprint);
        }

        [HttpPatch]
        [Route("upate")]
        public async Task<ActionResult<PATCHSprintDTO>> UpdatePlanning([FromBody] PATCHSprintDTO sprintDTO)
        {
            if (sprintDTO is null) { return NotFound(); }

            var sprint = await _sprintRepository.RecoverBy(p => p.SprintId == sprintDTO.SprintId);

            if (sprint is null) { return NotFound(); }

            sprint.Title = sprintDTO.Title ?? sprint.Title;
            sprint.Description = sprintDTO.Description ?? sprint.Description;
            sprint.Status = sprintDTO.Status ?? sprint.Status;
            sprint.EndDate = sprint.EndDate ?? sprint.EndDate;


            await _sprintRepository.UpdateAsync(sprint);

            return Ok(sprint);
        }
    }
}
