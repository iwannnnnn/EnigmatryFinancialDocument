namespace EnigmatryFinancialDocument.Core.DTOs
{
    public class TenantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInWhitelist { get; set; }
    }
}
