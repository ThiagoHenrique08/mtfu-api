namespace MoreThanFollowUp.Application.DTO.Project_DTO
{
    public class RequestProjectDTO
    {
        public POSTProject? Project { get; set; }
        public ICollection<POSTUserToProject>? UsersList { get; set; }
    }
}
