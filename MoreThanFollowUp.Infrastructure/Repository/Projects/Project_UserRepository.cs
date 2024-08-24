using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;

namespace MoreThanFollowUp.Infrastructure.Repository.Projects
{
    public class Project_UserRepository : DAL<Project_User>, IProject_UserRepository
    {
        public Project_UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
