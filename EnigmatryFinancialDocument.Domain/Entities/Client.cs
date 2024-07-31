namespace EnigmatryFinancialDocument.Core.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public Guid TenantId { get; set; }
        public string ClientVAT { get; set; }
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
        public bool IsInWhitelist { get; set; }
    }
}