namespace MoreThanFollowUp.Application.DTO.Project
{
    public class RequestProjectDTO
    {
        public POSTProjectDTO? Project { get; set; }
        public ICollection<POSTUserToProjectDTO>? UsersList { get; set; }
    }
}
