using EnigmatryFinancialDocument.Core.Entities;

namespace EnigmatryFinancialDocument.Core.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<(Guid ClientId, string ClientVAT)> GetClientInfoAsync(Guid tenantId, Guid documentId);
        Task<bool> IsClientWhitelistedAsync(Guid tenantId, Guid clientId);
        Task<Client?> GetClientAdditionalInfoAsync(string clientVAT);
        Task<IEnumerable<Client>> GetAllAsync();
    }
}
