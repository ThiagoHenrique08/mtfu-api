using MoreThanFollowUp.Domain.Entities.Sprints.Tasks;

namespace MoreThanFollowUp.Domain.Entities.Sprints
{
    public class SprintDesignPhase : Sprint
    {

        public virtual ICollection<Task_SprintDesignPhase>? Task_SprintDesignPhase { get; set; }
    }
}
