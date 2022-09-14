using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Message;

namespace SampleCQRS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoundController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public RoundController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRound([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetRoundQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetRounds()
        {
            return Ok(await _mediator.Send(new GetRoundQuery { }));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRound([FromBody] RoundRequest RoundRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateRoundCommand { Round = RoundRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRound([FromRoute] int id, [FromBody] RoundRequest RoundRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateRoundCommand { Id = id, Round = RoundRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRound([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteRoundCommand { Id = id });

            return Ok(result);
        }
    }
}
