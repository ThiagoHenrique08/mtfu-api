namespace MoreThanFollowUp.Application.DTO.Project_DTO
{
    public class RequestProjectDTO
    {
        public POSTProjectDTO? Project { get; set; }
        public ICollection<POSTUserToProjectDTO>? UsersList { get; set; }
    }
}
