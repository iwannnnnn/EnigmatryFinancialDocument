using EnigmatryFinancialDocument.Application.Handlers.Client.Query;
using EnigmatryFinancialDocument.Application.Handlers.Tenant.Query;
using EnigmatryFinancialDocument.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatryFinancialDocument.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : BaseController
    {

        public ClientController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClientsQuery query) 
            => await SendAsync<GetAllClientsQuery, IEnumerable<ClientDto>>(query);
    }
}