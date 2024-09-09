using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
using X.PagedList;


namespace MoreThanFollowUp.Infrastructure.Interfaces.Projects
{
    public interface IProjectRepository : IEFRepository<Project>
    {
        public Task<ICollection<Project>> PesquisarPorNome(string? title);
        
        Task<IPagedList<Project>> GetProjectPaginationAsync(ProjectsParameters projectsParameters);
    }
}
