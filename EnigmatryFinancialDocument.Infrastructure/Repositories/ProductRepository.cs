using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnigmatryFinancialDocument.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> IsProductSupportedAsync(string productCode) 
            => await _context.Products.AnyAsync(p => p.ProductCode == productCode);

        async Task<bool> IProductRepository.IsProductSupportedAsync(string productCode) 
            => await IsProductSupportedAsync(productCode);
    }

}
