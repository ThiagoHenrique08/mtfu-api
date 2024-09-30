using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Application.DTO.Project
{
    public class PATCHProjectDTO
    {
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Responsible { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
