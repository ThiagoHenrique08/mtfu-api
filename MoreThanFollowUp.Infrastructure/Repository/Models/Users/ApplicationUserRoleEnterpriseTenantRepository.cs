using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Repository.Models.Users
{
    public class ApplicationUserRoleEnterpriseTenantRepository : DAL<ApplicationUserRoleEnterpriseTenant>, IApplicationUserRoleEnterpriseTenantRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRoleEnterpriseTenantRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task RemoveRange(IEnumerable<ApplicationUserRoleEnterpriseTenant> listObjects)
        {
            _context.Set<ApplicationUserRoleEnterpriseTenant>().RemoveRange(listObjects);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUserRoleEnterpriseTenant> SearchForAsync(Expression<Func<ApplicationUserRoleEnterpriseTenant, bool>> condicao)
        {

            return _context.Set<ApplicationUserRoleEnterpriseTenant>().AsQueryable().Where(condicao);
        }
    }
}
