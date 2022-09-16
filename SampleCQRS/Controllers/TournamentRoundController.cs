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
    public class TournamentRoundController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public TournamentRoundController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournamentRound([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetTournamentRoundQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetTournamentRounds()
        {
            return Ok(await _mediator.Send(new GetTournamentRoundQuery { }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddTournamentRound([FromBody] TournamentRoundRequest TournamentRoundRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentRoundCommand { TournamentRound = TournamentRoundRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateTournamentRound([FromRoute] int id, [FromBody] TournamentRoundRequest TournamentRoundRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentRoundCommand { Id = id, TournamentRound = TournamentRoundRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteTournamentRound([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTournamentRoundCommand { Id = id });

            return Ok(result);
        }
    }
}
