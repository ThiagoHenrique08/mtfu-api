using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
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

        public async Task<ICollection<Project>> SearchByName(string? title)
        {
            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(p => p.Title!.Contains(title));
            }
            var results = await query.ToListAsync();

            return results;
        }

        public async Task<IPagedList<Project>> GetProjectPaginationAsync(ProjectsParameters projectsParametersPagination, string parameter, string category, string status)
        {
            var projects = await GetAllWithParameters(parameter, category, status);
            var projectsOrdened = projects.OrderBy(p => p.ProjectId).AsQueryable();
            //var projectsOrdened = projects.OrderBy(p => p.ProjectId).AsQueryable().Include(p=>p.Projects_Users!.Select(u=>u.User!.CompletedName));
            //var ordenedProjects =  PagedList<Project>.ToPagedList(projects, projectsParameters.PageNumber, projectsParameters.PageSize);
            
            var projectsResult =  projectsOrdened.ToPagedList(projectsParametersPagination.PageNumber, projectsParametersPagination.PageSize);
            
            return projectsResult;
        }

        public async Task<ICollection<Project>> GetAllWithParameters(string? parameter, string? category, string? status)
        {
            var query =  _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(parameter))
            {
                query = query.Where(p => p.Description!.Contains(parameter) || p.Title!.Contains(parameter));

            }


            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p=>p.Category!.Contains(category));
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status!.Contains(status));
            }
            var results = await query.ToListAsync();

            return results;
        }
    }
}
