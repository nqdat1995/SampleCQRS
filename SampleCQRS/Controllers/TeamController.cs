using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Notify;
using SampleCQRSApplication.Query;

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
        public async Task<IActionResult> GetTeams()
        {
            var result = await _mediator.Send(new GetTeamsQuery { });

            return Ok(result.Select(x => x.Name));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromBody] Team team)
        {
            var result = await _mediator.Send(new AddOrUpdateTeamCommand { Team = team });

            await _mediator.Publish(new PublishTeamNoify() { Message = $"Team {team.Name} created" });

            return Ok(result);
        }
    }
}
