namespace EnigmatryFinancialDocument.Core.Entities
{
    public class FinancialDocument
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public Guid DocumentId { get; set; }
        public Guid TenantId { get; set; }
        public string ProductCode { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Guid ClientId { get; set; }
    }
}