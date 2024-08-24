using MoreThanFollowUp.Domain.Entities.Sprints.Tasks;

namespace MoreThanFollowUp.Domain.Entities.Sprints
{
    public class SprintDeploymentPhase : Sprint
    {
        public virtual ICollection<Task_SprintDeploymentPhase>? Task_SprintDeploymentPhase { get; set; }
    }
}
