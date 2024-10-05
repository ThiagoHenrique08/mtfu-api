using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Projects
{
    public class Sprint_UserRepository : DAL<Sprint_User>, ISprint_UserRepository
    {
        private readonly ApplicationDbContext _context;
        public Sprint_UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveRange(IEnumerable<Sprint_User> listObjects)
        {
            _context.Set<Sprint_User>().RemoveRange(listObjects);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Sprint_User> SearchForAsync(Expression<Func<Sprint_User, bool>> condicao)
        {

            return _context.Set<Sprint_User>().AsQueryable().Where(condicao);
        }
    }
}