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
    public class SeasonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork unitOfWork;

        public SeasonController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetMatchs()
        {
            return Ok(await _mediator.Send(new GetSeasonQuery { }));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSeason([FromBody] SeasonRequest seasonRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateSeasonCommand { Season = seasonRequest });

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateMatch([FromRoute] int id, [FromBody] SeasonRequest seasonRequest)
        {
            var result = await _mediator.Send(new AddOrUpdateSeasonCommand { Id = id, Season = seasonRequest });

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMatch([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSeasonCommand { Id = id });

            return Ok(result);
        }
    }
}
