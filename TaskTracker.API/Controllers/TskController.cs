using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Tasks.Commands;
using TaskTracker.Application.Tasks.Queries;
using TaskTracker.Domain.Entities;

namespace TaskTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Tsk[]>> Get([FromQuery] GetTskAllQuery request, CancellationToken token)
        {
            IEnumerable<Tsk> result = await _mediator.Send(request, token);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateTskCommand command, CancellationToken token)
        {
            int id = await _mediator.Send(command, token);

            return Ok(id);
        }
    }
}
