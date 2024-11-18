using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Domain.Entities.Resources
{
    public class ProjectCategory
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
    }
}
