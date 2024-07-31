namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public interface ITenantService
    {
        Task IsTenantWhitelistedAsync(Guid tenantId);
    }
}
