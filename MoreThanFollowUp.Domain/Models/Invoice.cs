namespace MoreThanFollowUp.Domain.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public double? Amount { get; set; }
        public string? Status { get; set; }
        public int? SubscriptionId { get; set; }
        public virtual Subscription? Subscription { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? DueDate { get; set; }


    }
}
