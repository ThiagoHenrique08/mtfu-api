namespace MoreThanFollowUp.Application.DTO.Enterprise
{
    public class POSTEntepriseDTO
    {
        public string? CorporateReason { get; set; }
        public string? CNPJ { get; set; }
        public string? Segment { get; set; }
        public Guid? TenantId { get; set; }

    }
}
