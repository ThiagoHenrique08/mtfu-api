using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Project.Planning
{
    public class PlanningDTO
    {
        public int PlanningId { get; set; }
        public string? DocumentationLink { get; set; }
        public string? PlanningDescription { get; set; }
    }
}
