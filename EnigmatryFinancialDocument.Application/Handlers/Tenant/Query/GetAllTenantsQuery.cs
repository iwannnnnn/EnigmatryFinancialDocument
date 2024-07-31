using EnigmatryFinancialDocument.Core.DTOs;
using MediatR;

namespace EnigmatryFinancialDocument.Application.Handlers.Tenant.Query
{
    public class GetAllTenantsQuery : IRequest<IEnumerable<TenantDto>>
    {
    }

}