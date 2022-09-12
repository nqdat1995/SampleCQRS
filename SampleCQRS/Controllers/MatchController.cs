using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Notify;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Request;

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
        [HttpGet]
        public async Task<IActionResult> GetMatchs([FromQuery] int roundId = 0, [FromQuery] int homeTeamId = 0, [FromQuery] int visitingTeamId = 0)
        {
            if (roundId != 0 || homeTeamId != 0 || visitingTeamId != 0)
                return Ok(await _mediator.Send(new GetMatchQuery { RoundId = roundId, HomeTeamId = homeTeamId, VisitingTeamId = visitingTeamId }));
            else
                return Ok((await _mediator.Send(new GetMatchQuery { })).FirstOrDefault());
        }
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> AddMatch([FromBody] MatchRequest team)
        //{
        //    var result = await _mediator.Send(new AddOrUpdateMatchCommand { Match = team });

        //    return Ok(result);
        //}
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateMatch([FromRoute] int id, [FromBody] MatchRequest team)
        //{
        //    var result = await _mediator.Send(new AddOrUpdateMatchCommand { Match = team });

        //    return Ok(result);
        //}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMatch([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteMatchCommand { Id = id });

            return Ok(result);
        }
    }
}
