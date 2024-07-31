namespace EnigmatryFinancialDocument.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsProductSupportedAsync(string productCode);
    }

}
