using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Exceptions;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnigmatryFinancialDocument.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        async Task<(Guid ClientId, string ClientVAT)> IClientRepository.GetClientInfoAsync(Guid ClientId, Guid documentId) 
            => await GetClientInfoAsync(ClientId, documentId);

        async Task<bool> IClientRepository.IsClientWhitelistedAsync(Guid ClientId, Guid clientId) 
            => await IsClientWhitelistedAsync(ClientId, clientId);

        async Task<Client?> IClientRepository.GetClientAdditionalInfoAsync(string clientVAT) 
            => await GetClientAdditionalInfoAsync(clientVAT);

        private async Task<(Guid ClientId, string ClientVAT)> GetClientInfoAsync(Guid ClientId, Guid documentId)
        {
            var document = await _context.FinancialDocuments
                .Include(fd => fd.Transactions)
                .FirstOrDefaultAsync(fd => fd.ClientId == ClientId && fd.DocumentId == documentId)
                ?? throw new EnigmatryFinancialDocumentNotFoundException("Document not found");
            
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == document.ClientId);
            return client == null ? throw new EnigmatryFinancialDocumentNotFoundException("Client not found") : (client.ClientId, client.ClientVAT);
        }

        private async Task<bool> IsClientWhitelistedAsync(Guid ClientId, Guid clientId) 
            => await _context.Clients.AnyAsync(wc => wc.ClientId == ClientId && wc.ClientId == clientId && wc.IsInWhitelist);

        private async Task<Client?> GetClientAdditionalInfoAsync(string clientVAT) =>
            await _context.Clients.FirstOrDefaultAsync(c => c.ClientVAT == clientVAT);

        private async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.ToListAsync();

        async Task<IEnumerable<Client>> IClientRepository.GetAllAsync() => await GetAllAsync();
    }
}
