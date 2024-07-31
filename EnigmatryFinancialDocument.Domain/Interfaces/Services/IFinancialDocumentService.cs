

using EnigmatryFinancialDocument.Core.Entities;

namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public interface IFinancialDocumentService
    {
        Task<FinancialDocument> GetFinancialDocumentAsync(Guid tenantId, Guid documentId, string productCode);
    }
}