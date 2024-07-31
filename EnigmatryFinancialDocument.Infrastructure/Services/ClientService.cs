using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Exceptions;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using System.Data;

namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        async Task<(string RegistrationNumber, string CompanyType)> IClientService.GetValidateClient(Guid tenantId, Guid documentId) => await ValidateClient(tenantId, documentId);

        private async Task<(Guid ClientId, string ClientVAT)> GetClientInfoAsync(Guid tenantId, Guid documentId) 
            => await _clientRepository.GetClientInfoAsync(tenantId, documentId);

        private async Task IsClientWhitelistedAsync(Guid tenantId, Guid clientId)
        {
            if (await _clientRepository.IsClientWhitelistedAsync(tenantId, clientId))
            {
                return;
            }
            throw new EnigmatryFinancialDocumentNotFoundException($"{clientId} is not whitelisted.");
        }

        private async Task<(string RegistrationNumber, string CompanyType)> GetClientAdditionalInfoAsync(string clientVAT)
        {
            var client =await _clientRepository.GetClientAdditionalInfoAsync(clientVAT);
            return client == null ? throw new EnigmatryFinancialDocumentNotFoundException("Client not found") : (client.RegistrationNumber, client.CompanyType);

        }

        private async Task<(string RegistrationNumber, string CompanyType)> ValidateClient(Guid tenantId, Guid documentId)
        {

            var (ClientId, ClientVAT) = await GetClientInfoAsync(tenantId, documentId);
            await IsClientWhitelistedAsync(tenantId, ClientId);

            var (registrationNumber, companyType) = await  GetClientAdditionalInfoAsync(ClientVAT);

            return companyType.Equals("small", StringComparison.OrdinalIgnoreCase)
                ? throw new EnigmatryFinancialDocumentNotFoundException("Company type 'small' is not allowed.")
                : ((string RegistrationNumber, string CompanyType))(registrationNumber, companyType);
        }

    }
}
