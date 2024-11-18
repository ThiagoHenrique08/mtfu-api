using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Enterprise
{
    public class PATCHEnterpriseDTO
    {
        public Guid EnterpriseId { get; set; }
        public string? CorporateReason { get; set; }
        public string? CNPJ { get; set; }
        public string? Segment { get; set; }
    }
}
