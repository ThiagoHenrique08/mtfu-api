using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;

namespace MoreThanFollowUp.Infrastructure.Repository.Models.Users
{
    public class ApplicationUserRoleEnterpriseRepository : DAL<ApplicationUserRoleEnterprise>, IApplicationUserRoleEnterpriseRepository
    {
        public ApplicationUserRoleEnterpriseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
