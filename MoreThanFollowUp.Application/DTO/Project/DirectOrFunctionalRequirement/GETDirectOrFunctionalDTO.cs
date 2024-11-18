using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Project.DirectOrFunctionalRequirement
{
    public class GETDirectOrFunctionalDTO
    {
        public Guid DirectOrFunctionalRequirementId { get; set; }
        public string? FunctionOrAction { get; set; }

        public string? SystemBehavior { get; set; }
    }
}
