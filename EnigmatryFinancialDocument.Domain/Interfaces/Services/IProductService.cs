namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public interface IProductService
    {
        Task ValidateProductAsync(string productCode);
    }
}
