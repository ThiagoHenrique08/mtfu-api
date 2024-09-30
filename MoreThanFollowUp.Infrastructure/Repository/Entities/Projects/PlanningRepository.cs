using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using System.Linq.Expressions;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Projects
{
    public class PlanningRepository : DAL<Planning>, IPlanningRepository
    {
        public PlanningRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
