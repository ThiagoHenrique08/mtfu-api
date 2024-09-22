using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Resources
{
    public class ProjectCategoryRepository : DAL<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
