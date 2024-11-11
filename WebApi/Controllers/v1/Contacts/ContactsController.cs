using Application.Features.Contacts.Command;
using Application.Features.Contacts.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.Contacts
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateContactCommand request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(request, cancellationToken));

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateContactCommand request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(request, cancellationToken));

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(DeleteContactCommand request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(request, cancellationToken));

        [HttpGet("GetByPagination")]
        public async Task<IActionResult> GetByPagination([FromQuery] GetContactsWIthPagination request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(request, cancellationToken));
    }
}
