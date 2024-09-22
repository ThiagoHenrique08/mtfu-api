using MoreThanFollowUp.Domain.Entities.Projects;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects
{
    public interface IProject_UserRepository : IEFRepository<Project_User>
    {
        public Task RemoveRange(IEnumerable<Project_User> listObjects);
        public IEnumerable<Project_User> SearchForAsync(Expression<Func<Project_User, bool>> condicao);
    }
}
