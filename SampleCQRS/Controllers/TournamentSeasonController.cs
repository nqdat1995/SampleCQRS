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
    public class TournamentSeasonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public TournamentSeasonController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournamentSeason([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetTournamentSeasonQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetTournamentSeasons()
        {
            return Ok(await _mediator.Send(new GetTournamentSeasonQuery { }));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTournamentSeason([FromBody] TournamentSeasonRequest TournamentSeasonRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentSeasonCommand { TournamentSeason = TournamentSeasonRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTournamentSeason([FromRoute] int id, [FromBody] TournamentSeasonRequest TournamentSeasonRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentSeasonCommand { Id = id, TournamentSeason = TournamentSeasonRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTournamentSeason([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTournamentSeasonCommand { Id = id });

            return Ok(result);
        }
    }
}
