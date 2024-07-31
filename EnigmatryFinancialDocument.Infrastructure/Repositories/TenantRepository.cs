using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnigmatryFinancialDocument.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Tenant>> ITenantRepository.GetAllAsync() =>await GetAllAsync();

        async Task<bool> ITenantRepository.IsTenantWhitelistedAsync(Guid tenantId)
            => await IsTenantWhitelistedAsync(tenantId);

        private async Task<bool> IsTenantWhitelistedAsync(Guid tenantId) 
            => await _context.Tenants.AnyAsync(t => t.TenantId == tenantId && t.IsInWhitelist);

        private async Task<IEnumerable<Tenant>> GetAllAsync() => await _context.Tenants.ToListAsync();

    }
}
