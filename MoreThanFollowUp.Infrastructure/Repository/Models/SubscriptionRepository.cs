using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Repository.Models
{
    public class SubscriptionRepository : DAL<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
