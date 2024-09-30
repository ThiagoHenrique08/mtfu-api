using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Projects
{
    public class SprintRepository : DAL<Sprint>, ISprintRepository
    {
        public SprintRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

