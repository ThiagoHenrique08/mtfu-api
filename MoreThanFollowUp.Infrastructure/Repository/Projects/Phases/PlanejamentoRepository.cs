using MoreThanFollowUp.Domain.Entities.Projects.Phases;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects.Phases;


namespace MoreThanFollowUp.Infrastructure.Repository.Projects.Phases
{
    public class PlanejamentoRepository : DAL<PlanningPhase>, IPlanejamentoRepository
    {
        public PlanejamentoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
