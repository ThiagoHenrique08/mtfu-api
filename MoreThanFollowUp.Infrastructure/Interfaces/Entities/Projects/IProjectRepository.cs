﻿using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Pagination;
using X.PagedList;


namespace MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects
{
    public interface IProjectRepository : IEFRepository<Project>
    {
        Task<ICollection<Project>> SearchByName(string? title);

        Task<IPagedList<Project>> GetProjectPaginationAsync(ProjectsParameters projectsParametersPagination, string paramenter, string category, string status, Guid enterpriseId);

        Task<ICollection<Project>> GetAllWithParameters(string parameter, string category, string status, Guid enterpriseId);
    }
}
