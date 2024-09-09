using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
using X.Extensions.PagedList.EF;
using X.PagedList;
using X.PagedList.Extensions;


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

        public async Task<IPagedList<Project>> GetProjectPaginationAsync(ProjectsParameters projectsParameters)
        {
            var projects = await ListarAsync();
            var projectsOrdened = projects.OrderBy(p => p.ProjectId).AsQueryable();
            //var projectsOrdened = projects.OrderBy(p => p.ProjectId).AsQueryable().Include(p=>p.Projects_Users!.Select(u=>u.User!.CompletedName));
            //var ordenedProjects =  PagedList<Project>.ToPagedList(projects, projectsParameters.PageNumber, projectsParameters.PageSize);
            
            var projectsResult =  projectsOrdened.ToPagedList(projectsParameters.PageNumber, projectsParameters.PageSize);
            
            return projectsResult;
        }

    }
}
