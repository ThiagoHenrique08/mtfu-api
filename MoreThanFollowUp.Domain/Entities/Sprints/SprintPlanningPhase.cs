using MoreThanFollowUp.Domain.Entities.Projects.Phases;
using MoreThanFollowUp.Domain.Entities.Sprints.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Domain.Entities.Sprints
{
    public class SprintPlanningPhase : Sprint
    {
        
        public int PlanningPhaseId { get; set; }
        public virtual PlanningPhase? PlanningPhase { get; set; }

        //public virtual ICollection<Task_SprintPlanningPhase>? Task_SprintPlanningPhase { get; set; }
    }
}
