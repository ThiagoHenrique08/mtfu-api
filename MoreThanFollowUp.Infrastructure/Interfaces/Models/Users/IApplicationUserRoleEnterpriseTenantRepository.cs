using MoreThanFollowUp.Domain.Models;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Interfaces.Models.Users
{
    public interface IApplicationUserRoleEnterpriseTenantRepository : IEFRepository<ApplicationUserRoleEnterpriseTenant>
    {
        public Task RemoveRange(IEnumerable<ApplicationUserRoleEnterpriseTenant> listObjects);
        public IEnumerable<ApplicationUserRoleEnterpriseTenant> SearchForAsync(Expression<Func<ApplicationUserRoleEnterpriseTenant, bool>> condicao);
    }
}
