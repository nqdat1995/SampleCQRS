using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Request;

namespace SampleCQRS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public TeamController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            return Ok((await _mediator.Send(new GetTeamQuery { Id = id })).FirstOrDefault());
        }
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            return Ok(await _mediator.Send(new GetTeamQuery { }));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTeam([FromBody] TeamRequest TeamRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTeamCommand { Team = TeamRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTeam([FromRoute] int id, [FromBody] TeamRequest TeamRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateTeamCommand { Id = id, Team = TeamRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTeam([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTeamCommand { Id = id });

            return Ok(result);
        }
    }
}
