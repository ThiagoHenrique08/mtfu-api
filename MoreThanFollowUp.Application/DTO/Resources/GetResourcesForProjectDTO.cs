using MoreThanFollowUp.Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Resources
{
    public class GetResourcesForProjectDTO
    {
        public List<GetUsersDTO> Users { get; set; } = new List<GetUsersDTO>();
        public List<GetResponsibleDTO> Responsibles { get; set; } = new List<GetResponsibleDTO>();
        public List<GetCategoryDTO> Categories { get; set; } = new List<GetCategoryDTO>();
        public List<GETStatusDTO> StatusProjects { get; set; } = new List<GETStatusDTO>();
    }
}
