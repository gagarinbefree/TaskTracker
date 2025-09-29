using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.LogAttributes;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Domain.Entities;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [LogAction]
    [LogException]
    public class TskRelationshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TskRelationshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TskRelationshipDto[]>> Get([FromQuery] GetTskRelationshipAllQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TskRelationshipDto>> Get([FromQuery] GetTskRelationshipByIdQuery request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return result != null ? Ok(result) : NotFound(request);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateTskRelationshipCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteTskRelationshipCommand request, CancellationToken token)
        {
            bool result = await _mediator.Send(request, token);

            return result ? NoContent() : NotFound(request);
        }
    }
}
