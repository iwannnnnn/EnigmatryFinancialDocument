using EnigmatryFinancialDocument.Core.Exceptions;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;

namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        async Task IProductService.ValidateProductAsync(string productCode) 
            => await ValidateProductAsync(productCode);

        private async Task ValidateProductAsync(string productCode)
        {
            if (!await _productRepository.IsProductSupportedAsync(productCode))
            {
                throw new EnigmatryFinancialDocumentNotFoundException("Product not supported");
               
            }
        }

    }
}
