using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Entities.FinDoc;
using EnigmatryFinancialDocument.Core.Exceptions;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;

namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public class FinancialDocumentService : IFinancialDocumentService
    {
        private readonly IAnonymizationService _anonymizationService;
        private readonly IFinancialDocumentRepository _financialDocumentRepository;

        public FinancialDocumentService(IAnonymizationService anonymizationService, IFinancialDocumentRepository financialDocumentRepository)
        {
            _anonymizationService = anonymizationService;
            _financialDocumentRepository = financialDocumentRepository;
        }

        async Task<FinancialDocument> IFinancialDocumentService.GetFinancialDocumentAsync(Guid tenantId, Guid documentId, string productCode)
               => await GetFinancialDocumentAsync(tenantId, documentId, productCode);

        private async Task<FinancialDocument> GetFinancialDocumentAsync(Guid tenantId, Guid documentId, string productCode)
        {
            var document = await _financialDocumentRepository.GetFinancialDocumentAsync(tenantId, documentId) ?? throw new EnigmatryFinancialDocumentNotFoundException("Financial document not found.");

            if (!Enum.TryParse(productCode, out BuilderSupportedProductCode supportproductCode))
            {
                throw new ArgumentException($"Unsupported product code: {productCode}");
            }
            var builder = FinancialDocumentFactory.CreateBuilder(supportproductCode);
           
            return _anonymizationService.Anonymize(builder.Build(document));

        }
    }
}