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
    public class MatchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public MatchController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatch([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetMatchQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetMatchs()
        {
            return Ok(await _mediator.Send(new GetMatchQuery { }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddMatch([FromBody] MatchRequest MatchRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateMatchCommand { Match = MatchRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateMatch([FromRoute] int id, [FromBody] MatchRequest MatchRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateMatchCommand { Id = id, Match = MatchRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteMatch([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteMatchCommand { Id = id });

            return Ok(result);
        }
        [HttpPost("{matchId}/bet")]
        //[Authorize]
        public async Task<IActionResult> PlaceABet([FromRoute] int matchId, BetRequest request)
        {
            var result = await _mediator.Send(new AddOrUpdateBetCommand { MatchId = matchId, Bet = request });

            return Ok(result);
        }
    }
}
