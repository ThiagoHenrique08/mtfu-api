using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Projects
{
    public class RequirementAnalysisRepository : DAL<RequirementAnalysis>, IRequirementAnalysisRepository
    {
        public RequirementAnalysisRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
