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
    public class TournamentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public TournamentController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetTournamentQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            return Ok(await _mediator.Send(new GetTournamentQuery { }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddTournament([FromBody] TournamentRequest TournamentRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentCommand { Tournament = TournamentRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateTournament([FromRoute] int id, [FromBody] TournamentRequest TournamentRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentCommand { Id = id, Tournament = TournamentRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteTournament([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTournamentCommand { Id = id });

            return Ok(result);
        }
        [HttpGet("{tournamentId}/teams")]
        public async Task<IActionResult> GetTeamByTournament([FromRoute] int tournamentId)
        {
            return Ok(await _mediator.Send(new GetTeamByTournamentQuery { TournamentId = tournamentId }));
        }
        [HttpGet("{tournamentId}/rounds")]
        public async Task<IActionResult> GetRoundByTournament([FromRoute] int tournamentId)
        {
            return Ok(await _mediator.Send(new GetRoundByTournamentQuery { TournamentId = tournamentId }));
        }
    }
}
