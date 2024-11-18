using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Enterprise
{
    public class GETRolePerUserPerEntepriseDTO
    {
        public virtual ICollection<string>? Role_User_Enterprise { get; set; }
    }
}
