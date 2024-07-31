using EnigmatryFinancialDocument.Core.Entities;


namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public interface IAnonymizationService
    {
        FinancialDocument Anonymize(FinancialDocument document);
    }
}
