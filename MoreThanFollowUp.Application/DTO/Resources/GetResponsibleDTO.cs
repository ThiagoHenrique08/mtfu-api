using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Resources
{
    public class GetResponsibleDTO
    {
        public Guid ResponsibleId { get; set; }
        public string? Name { get; set; }
    }
}
