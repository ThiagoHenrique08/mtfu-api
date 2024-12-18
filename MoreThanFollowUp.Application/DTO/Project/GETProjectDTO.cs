﻿using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Application.DTO.Project
{
    public class GETProjectDTO
    {

        public Guid ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Responsible { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<string>? Projects_Users { get; set; }

    }
}
