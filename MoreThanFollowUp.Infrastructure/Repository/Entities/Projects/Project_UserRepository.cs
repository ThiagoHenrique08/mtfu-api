using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Projects
{
    public class Project_UserRepository : DAL<Project_User>, IProject_UserRepository
    {
        private readonly ApplicationDbContext _context;
        public Project_UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveRange(IEnumerable<Project_User> listObjects)
        {
            _context.Set<Project_User>().RemoveRange(listObjects);
            await _context.SaveChangesAsync();
        }

        public  IEnumerable<Project_User> SearchForAsync(Expression<Func<Project_User, bool>> condicao)
        {

            return  _context.Set<Project_User>().AsQueryable().Where(condicao);
        }
    }
}
