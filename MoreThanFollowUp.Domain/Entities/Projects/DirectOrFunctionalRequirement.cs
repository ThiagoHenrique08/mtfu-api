using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class DirectOrFunctionalRequirement
    {
        public Guid DirectOrFunctionalRequirementId { get; set; } = Guid.NewGuid();
        public string? FunctionOrAction { get; set; }

        public string? SystemBehavior { get; set; }

        public Guid? RequirementAnalysisId { get; set; }

        public virtual RequirementAnalysis? RequirementAnalysis { get; set; }
    }
}
