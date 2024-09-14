using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Repository.Users
{
    public class UserApplicationRepository : DAL<ApplicationUser>, IUserApplicationRepository
    {
        public UserApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
