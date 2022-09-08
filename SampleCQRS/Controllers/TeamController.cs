using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Notify;
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
        [HttpGet]
        public async Task<IActionResult> GetTeams([FromQuery] string? name = "")
        {
            if (string.IsNullOrEmpty(name))
                return Ok(await _mediator.Send(new GetTeamsQuery { }));
            else
                return Ok((await _mediator.Send(new GetTeamsQuery
                {
                    Name = name
                })).FirstOrDefault());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTeam([FromBody] TeamRequest team)
        {
            var result = await _mediator.Send(new AddOrUpdateTeamCommand { Team = team });

            await _mediator.Publish(new PublishTeamNoify() { Message = $"Team {team.Name} created" });

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTeam([FromRoute] int id, [FromBody] TeamRequest team)
        {
            var result = await _mediator.Send(new AddOrUpdateTeamCommand { Team = team });

            if (result)
                await _mediator.Publish(new PublishTeamNoify() { Message = $"Team {team.Name} updated" });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTeam([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteTeamCommand { Id = id });

            await _mediator.Publish(new PublishTeamNoify() { Message = $"Team {id} deleted" });

            return Ok(result);
        }
    }
}
