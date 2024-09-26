using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure
{
    public class DAL<T> : IEFRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public DAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> RegisterAsync(T objeto)
        {
            await _context.Set<T>().AddAsync(objeto);
            await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task<T> UpdateAsync(T objeto)
        {
            _context.Set<T>().Update(objeto);
            await _context.SaveChangesAsync();
            return objeto;
        }

        public async Task DeleteAsync(T objeto)
        {
            _context.Set<T>().Remove(objeto);
            await _context.SaveChangesAsync();
        }

        public ICollection<T> ToList()
        {
            var Lista = _context.Set<T>().ToList();
            return Lista;
        }
        public async Task<ICollection<T>> ToListAsync()
        {
            var Lista = await _context.Set<T>().ToListAsync();
            return Lista;
        }

        public async Task<T?> RecoverBy(Expression<Func<T, bool>> condicao)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(condicao);

        }

        public async Task RegisterList(ICollection<T> listObjects)
        {
            await _context.AddRangeAsync(listObjects);
            await _context.SaveChangesAsync();
        }

 
    }
}