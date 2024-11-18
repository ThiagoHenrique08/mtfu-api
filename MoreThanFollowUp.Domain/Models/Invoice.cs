namespace MoreThanFollowUp.Domain.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; } = Guid.NewGuid();
        public double? Amount { get; set; }
        public string? Status { get; set; }
        public Guid? SubscriptionId { get; set; }
        public virtual Subscription? Subscription { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? DueDate { get; set; }


    }
}
