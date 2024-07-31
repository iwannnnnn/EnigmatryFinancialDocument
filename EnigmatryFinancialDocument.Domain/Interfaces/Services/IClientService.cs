namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public interface IClientService
    {
         Task<(string RegistrationNumber, string CompanyType)> GetValidateClient(Guid tenantId, Guid documentId);
    }
}
