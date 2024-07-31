using EnigmatryFinancialDocument.Core.DTOs;

namespace EnigmatryFinancialDocument.Application.Handlers.FinancialDocument.Query
{
    public class FinancialDocumentResponse
    {
        public string Data { get; set; }
        public CompanyDto Company { get; set; }
    }
}
