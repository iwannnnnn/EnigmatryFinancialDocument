using EnigmatryFinancialDocument.Core.DTOs;
using EnigmatryFinancialDocument.Core.Interfaces;
using EnigmatryFinancialDocument.Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;

namespace EnigmatryFinancialDocument.Application.Handlers.FinancialDocument.Query
{
    public class GetFinancialDocumentQueryHandler : IRequestHandler<GetFinancialDocumentQuery, FinancialDocumentResponse>
    {
        private readonly IFinancialDocumentService _financialDocumentService;
        private readonly IProductService _productService;
        private readonly ITenantService _tenantService;
        private readonly IClientService _clientService;

        public GetFinancialDocumentQueryHandler(
            IFinancialDocumentService financialDocumentService,
            IProductService productService,
            ITenantService tenantService,
            IClientService clientService)
        {
            _financialDocumentService = financialDocumentService;
            _productService = productService;
            _tenantService = tenantService;
            _clientService = clientService;
        }

        public async Task<FinancialDocumentResponse> Handle(GetFinancialDocumentQuery request, CancellationToken cancellationToken)
        {
            await _productService.ValidateProductAsync(request.ProductCode);

            await _tenantService.IsTenantWhitelistedAsync(request.TenantId);

            var (registrationNumber, companyType) = await _clientService.GetValidateClient(request.TenantId, request.DocumentId);
            
            var financialDocument = await _financialDocumentService.GetFinancialDocumentAsync(request.TenantId, request.DocumentId, request.ProductCode);

            return new FinancialDocumentResponse
            {
                Data = JsonConvert.SerializeObject(financialDocument),
                Company = new CompanyDto
                {
                    CompanyType = companyType,
                    RegistrationNumber=registrationNumber
                }
            };
        }
    }

}