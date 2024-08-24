namespace MoreThanFollowUp.Application.DTO.Project_DTO
{
    public class GETProject
    {

        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Responsible { get; set; }
        public string? Category { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Description { get; set; }

    }
}
