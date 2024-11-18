using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Repository.Models
{
    public class Enterprise_UserRepository : DAL<Enterprise_User>, IEnterprise_UserRepository
    {
        private readonly ApplicationDbContext _context;
        public Enterprise_UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveRange(IEnumerable<Enterprise_User> listObjects)
        {
            _context.Set<Enterprise_User>().RemoveRange(listObjects);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Enterprise_User> SearchForAsync(Expression<Func<Enterprise_User, bool>> condicao)
        {

            return _context.Set<Enterprise_User>().AsQueryable().Where(condicao);
        }
    }
}
