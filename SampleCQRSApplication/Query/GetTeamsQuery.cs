using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetTeamsQuery : IRequest<List<Team>>
    {
    }
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<Team>>
    {
        private readonly TeamsInMemory teamsInMemory;

        public GetTeamsQueryHandler()
        {
            teamsInMemory = new TeamsInMemory();
        }

        public Task<List<Team>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = teamsInMemory.Teams;
            return Task.FromResult(teams);
        }
    }
}
