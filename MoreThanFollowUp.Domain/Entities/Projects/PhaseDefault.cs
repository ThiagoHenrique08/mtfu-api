using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class PhaseDefault
    {

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project? Project { get; set; }

        public virtual ICollection<Sprint>? Sprints { get; set; }
    }
}
