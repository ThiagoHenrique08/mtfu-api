using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Repository.Models.Users
{
    public class ApplicationRoleRepository : DAL<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
