using MoreThanFollowUp.Domain.Models;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Interfaces.Models
{
    public interface IEnterprise_UserRepository : IEFRepository<Enterprise_User>
    {
        public Task RemoveRange(IEnumerable<Enterprise_User> listObjects);
        public IEnumerable<Enterprise_User> SearchForAsync(Expression<Func<Enterprise_User, bool>> condicao);
    }
}
