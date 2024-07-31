using MediatR;

namespace EnigmatryFinancialDocument.Application.Handlers.FinancialDocument.Query
{
    public class GetFinancialDocumentQuery : IRequest<FinancialDocumentResponse>
    {
        public string ProductCode { get; set; }
        public Guid TenantId { get; set; }
        public Guid DocumentId { get; set; }
    }
}
