using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Resources;

namespace MoreThanFollowUp.Infrastructure.Repository.Resources
{
    public class ProjectCategoryRepository : DAL<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
