using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;

namespace MoreThanFollowUp.Infrastructure.Repository.Projects
{
    public class ProjectRepository : DAL<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Project>> PesquisarPorNome(string? title)
        {
            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(p => p.Title!.Contains(title));
            }
            var results = await query.ToListAsync();

            return results;
        }

        public PagedList<Project> GetProjectPagination(ProjectsParameters projectsParameters)
        {
            var projects = Listar().OrderBy(p => p.ProjectId).AsQueryable();
            var ordenedProjects = PagedList<Project>.ToPagedList(projects, projectsParameters.PageNumber, projectsParameters.PageSize);

            return ordenedProjects;
        }
    }
}
