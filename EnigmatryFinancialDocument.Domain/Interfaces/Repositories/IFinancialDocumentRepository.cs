using EnigmatryFinancialDocument.Core.Entities;

namespace EnigmatryFinancialDocument.Core.Interfaces.Repositories
{
    public interface IFinancialDocumentRepository
    {
        //Task<FinancialDocument> GetFinancialDocumentAsync(string productCode, Guid tenantId, Guid documentId);
        Task<FinancialDocument> GetFinancialDocumentAsync(Guid tenantId, Guid documentId);
    }
}
