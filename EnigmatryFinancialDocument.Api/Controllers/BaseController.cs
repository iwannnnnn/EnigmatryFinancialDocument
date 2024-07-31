using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatryFinancialDocument.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse> => Ok(await _mediator.Send(request));

        protected async Task<IActionResult> SendAsync<TRequest>(TRequest request)
            where TRequest : IRequest
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }

}