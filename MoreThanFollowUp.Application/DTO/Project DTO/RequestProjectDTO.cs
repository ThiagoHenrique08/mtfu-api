using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Project_DTO
{
    public class RequestProjectDTO
    {
        public POSTProject? Project { get; set; }
        public ICollection<POSTProjectUser>? ProjectUsers { get; set; }
    }
}
