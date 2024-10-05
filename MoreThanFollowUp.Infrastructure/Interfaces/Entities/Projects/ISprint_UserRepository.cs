using MoreThanFollowUp.Domain.Entities.Projects;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects
{
    public interface ISprint_UserRepository : IEFRepository<Sprint_User>
    {
        public Task RemoveRange(IEnumerable<Sprint_User> listObjects);
        public IEnumerable<Sprint_User> SearchForAsync(Expression<Func<Sprint_User, bool>> condicao);
    }
}
