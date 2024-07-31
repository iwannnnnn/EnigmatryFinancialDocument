using EnigmatryFinancialDocument.Core.Entities;

namespace EnigmatryFinancialDocument.Core.Interfaces.Repositories
{
    public interface ITenantRepository
    {
        Task<bool> IsTenantWhitelistedAsync(Guid tenantId);
        Task<IEnumerable<Tenant>> GetAllAsync();
    }

}
