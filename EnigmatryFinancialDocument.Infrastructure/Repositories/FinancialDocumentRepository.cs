using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnigmatryFinancialDocument.Infrastructure.Repositories
{
    public class FinancialDocumentRepository : IFinancialDocumentRepository
    {
        private readonly AppDbContext _context;

        public FinancialDocumentRepository(AppDbContext context)
        {
            _context = context;
        }


        async Task<Core.Entities.FinancialDocument> IFinancialDocumentRepository.GetFinancialDocumentAsync(Guid tenantId, Guid documentId) 
            => await GetFinancialDocumentAsync(tenantId, documentId);

        private async Task<FinancialDocument> GetFinancialDocumentAsync(Guid tenantId, Guid documentId) 
            => await _context.FinancialDocuments.FirstOrDefaultAsync(doc => doc.DocumentId == documentId && doc.TenantId == tenantId);
    }

}
