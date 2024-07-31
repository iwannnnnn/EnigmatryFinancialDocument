namespace EnigmatryFinancialDocument.Core.Entities
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInWhitelist { get; set; }
    }
}
