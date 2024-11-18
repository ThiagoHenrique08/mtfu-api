using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Projects
{
    public class DirectOrFunctionalRequirementRepository : DAL<DirectOrFunctionalRequirement>, IDirectOrFunctionalRequirementRepository
    {
        public DirectOrFunctionalRequirementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
