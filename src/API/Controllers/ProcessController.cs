using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.CreateProcess;
using Application.Queries.GetProcesses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<ProcessDTO>> Get()
        {
            return _mediator.Send(new GetProcessesQuery());
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(CreateProcessCommand createProcessCommand)
        {
            await _mediator.Send(createProcessCommand);
            return Ok();
        }
    }
}