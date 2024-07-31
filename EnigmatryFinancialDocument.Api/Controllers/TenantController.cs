using EnigmatryFinancialDocument.Application.Handlers.Tenant.Query;
using EnigmatryFinancialDocument.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatryFinancialDocument.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : BaseController
    {

        public TenantController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTenantsQuery query) => await SendAsync<GetAllTenantsQuery, IEnumerable<TenantDto>>(query);
    }
}