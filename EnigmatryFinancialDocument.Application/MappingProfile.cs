using AutoMapper;
using EnigmatryFinancialDocument.Core.DTOs;
using EnigmatryFinancialDocument.Core.Entities;

namespace EnigmatryFinancialDocument.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FinancialDocument, FinancialDocumentDto>();
            CreateMap<Tenant, TenantDto>();
        }
    }
}