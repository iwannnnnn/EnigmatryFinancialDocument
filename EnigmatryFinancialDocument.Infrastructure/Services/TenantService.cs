using EnigmatryFinancialDocument.Core.Exceptions;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;

namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        Task ITenantService.IsTenantWhitelistedAsync(Guid tenantId) => IsTenantWhitelistedAsync(tenantId);

        private async Task IsTenantWhitelistedAsync(Guid tenantId)
        {
            if (!await _tenantRepository.IsTenantWhitelistedAsync(tenantId))
            {

                throw new EnigmatryFinancialDocumentNotFoundException("Tenant not whitelisted");
            }
        }

    }
}
