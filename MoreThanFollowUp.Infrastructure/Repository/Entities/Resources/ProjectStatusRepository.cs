﻿using MoreThanFollowUp.Domain.Entities.Resources;
using MoreThanFollowUp.Infrastructure.Context;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Repository.Entities.Resources
{
    public class ProjectStatusRepository : DAL<ProjectStatus>, IProjectStatusRepository
    {
        public ProjectStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
