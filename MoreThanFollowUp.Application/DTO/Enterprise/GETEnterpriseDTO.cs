namespace MoreThanFollowUp.Application.DTO.Enterprise
{
    public class GETEnterpriseDTO
    {
        public Guid EnterpriseId { get; set; }
        public string? CorporateReason { get; set; }
        public string? CNPJ { get; set; }
        public string? Segment { get; set; }
        public Guid? TenantId { get; set; }
 
    }
}
