using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;

namespace MoreThanFollowUp.Infrastructure.Interfaces.Projects
{
    public interface IProjectRepository : IEFRepository<Project>
    {
        public Task<ICollection<Project>> PesquisarPorNome(string? title);

        public PagedList<Project> GetProjectPagination(ProjectsParameters projectsParameters);
    }
}
