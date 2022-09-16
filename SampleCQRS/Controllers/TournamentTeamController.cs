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
    public class TournamentTeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public TournamentTeamController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournamentTeam([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetTournamentTeamQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetTournamentTeams()
        {
            return Ok(await _mediator.Send(new GetTournamentTeamQuery { }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddTournamentTeam([FromBody] TournamentTeamRequest TournamentTeamRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentTeamCommand { TournamentTeam = TournamentTeamRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateTournamentTeam([FromRoute] int id, [FromBody] TournamentTeamRequest TournamentTeamRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTournamentTeamCommand { Id = id, TournamentTeam = TournamentTeamRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteTournamentTeam([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTournamentTeamCommand { Id = id });

            return Ok(result);
        }
    }
}
