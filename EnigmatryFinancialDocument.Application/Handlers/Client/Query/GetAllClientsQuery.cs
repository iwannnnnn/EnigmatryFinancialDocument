using EnigmatryFinancialDocument.Application.Handlers.Client.Query;
using EnigmatryFinancialDocument.Core.DTOs;
using MediatR;

namespace EnigmatryFinancialDocument.Application.Handlers.Client.Query
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
    }
}