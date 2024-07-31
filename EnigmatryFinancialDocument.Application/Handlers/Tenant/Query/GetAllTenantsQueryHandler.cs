using AutoMapper;
using EnigmatryFinancialDocument.Core.DTOs;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using MediatR;

namespace EnigmatryFinancialDocument.Application.Handlers.Tenant.Query
{
    public class GetAllTenantsQueryHandler : IRequestHandler<GetAllTenantsQuery, IEnumerable<TenantDto>>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper; 

        public GetAllTenantsQueryHandler(ITenantRepository tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TenantDto>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TenantDto>>(tenants);
        }
    }

}