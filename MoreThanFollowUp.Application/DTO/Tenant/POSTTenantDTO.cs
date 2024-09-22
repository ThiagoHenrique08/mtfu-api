using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Tenant
{
    public class POSTTenantDTO
    {
        public string? TenantName { get; set; }
        public string? TenantCustomDomain { get; set; }
        public string? TenantStatus { get; set; }
        public string? Responsible { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
