﻿using MoreThanFollowUp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Interfaces.Models
{
    public interface ITenantRepository : IEFRepository<Tenant>
    {
    }
}
