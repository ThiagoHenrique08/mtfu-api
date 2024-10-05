namespace MoreThanFollowUp.Application.DTO.Project.Sprint
{
    public class RequestSprintDTO
    {
        public POSTSprintDTO? Sprint { get; set; }
        public ICollection<POSTUserToSprintDTO>? UsersList { get; set; }
    }
}
