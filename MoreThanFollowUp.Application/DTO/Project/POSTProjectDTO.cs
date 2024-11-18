namespace MoreThanFollowUp.Application.DTO.Project
{
    public class POSTProjectDTO
    {
        public string? Title { get; set; }

        public string? Responsible { get; set; }
        public string? Category { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public Guid EnterpriseId { get; set; }

    }
}
