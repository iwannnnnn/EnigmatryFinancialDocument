using EnigmatryFinancialDocument.Application.Handlers.FinancialDocument.Query;
using EnigmatryFinancialDocument.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EnigmatryFinancialDocument.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class FinancialDocumentsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public FinancialDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Gets a financial document based on the provided query parameters.
        /// </summary>
        /// <param name="query">The query parameters for retrieving the financial document.</param>
        /// <returns>The financial document response.</returns>
        /// <response code="200">Returns the financial document</response>
        /// <response code="403">If the request is forbidden</response>
        /// <response code="404">If the document is not found</response>
        [HttpGet]
        [ProducesResponseType(typeof(FinancialDocumentResponse), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFinancialDocument([FromQuery] GetFinancialDocumentQuery query)
        {
            try
            {
                return Ok(await _mediator.Send(query));

            }
            catch (EnigmatryFinancialDocumentNotFoundException ex)
            {
                return Forbid(ex.Message);


            }

        }
    }
}