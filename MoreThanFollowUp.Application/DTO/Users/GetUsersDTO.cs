using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Users
{
    public class GetUsersDTO
    {
        public string? UserId { get; set; }
        public string? NameCompleted { get; set; }
        public string? Function { get; set; }
    }
}
