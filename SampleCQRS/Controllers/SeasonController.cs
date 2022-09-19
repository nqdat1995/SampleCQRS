using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Notify;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Message;

namespace SampleCQRS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public SeasonController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeason([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetSeasonQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetSeasons()
        {
            return Ok(await _mediator.Send(new GetSeasonQuery { }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddSeason([FromBody] SeasonRequest seasonRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateSeasonCommand { Season = seasonRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateSeason([FromRoute] int id, [FromBody] SeasonRequest seasonRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateSeasonCommand { Id = id, Season = seasonRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteSeason([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSeasonCommand { Id = id });

            return Ok(result);
        }
        [HttpGet("{seasonId}/tournaments")]
        public async Task<IActionResult> GetTournamentBySeason([FromRoute] int seasonId)
        {
            return Ok(await _mediator.Send(new GetTournamentBySeasonQuery { SeasonId = seasonId }));
        }
        [HttpPost("{seasonId}/tournament")]
        //[Authorize]
        public async Task<IActionResult> AddTournamentBySeason([FromRoute] int seasonId, [FromBody] TournamentRequest tournament)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentBySeasonCommand { Tournament = tournament, SeasonId = seasonId });

            return Ok(result);
        }
    }
}
