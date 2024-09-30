using MoreThanFollowUp.Domain.Entities.Projects;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Interfaces
{
    public interface IEFRepository<T> where T : class
    {
        public ICollection<T> ToList();
        public Task<ICollection<T>> ToListAsync();
        public Task<T> RegisterAsync(T objeto);

        public Task<T> UpdateAsync(T objeto);

        public Task DeleteAsync(T objeto);

        public Task<T?> RecoverBy(Expression<Func<T, bool>> condicao);

        public Task RegisterList(ICollection<T> listObjects);
        public IEnumerable<T> SearchForAsync(Expression<Func<T, bool>> condicao);



    }
}
