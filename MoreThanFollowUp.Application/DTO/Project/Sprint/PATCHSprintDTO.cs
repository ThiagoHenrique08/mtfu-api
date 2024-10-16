﻿namespace MoreThanFollowUp.Application.DTO.Project.Sprint
{
    public class PATCHSprintDTO
    {
        public Guid SprintId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? SprintScore { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }

    }
}
