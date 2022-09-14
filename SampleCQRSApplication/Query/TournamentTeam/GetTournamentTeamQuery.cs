using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetTournamentTeamQuery : IRequest<IEnumerable<TournamentTeam>>
    {
        public int Id { get; set; }
    }
    public class GetTournamentTeamQueryHandler : IRequestHandler<GetTournamentTeamQuery, IEnumerable<TournamentTeam>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTournamentTeamQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<TournamentTeam>> Handle(GetTournamentTeamQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.TournamentTeamRepository.Get());
            return Task.FromResult(unitOfWork.TournamentTeamRepository.Get(x => x.Id == request.Id));
        }
    }
}
