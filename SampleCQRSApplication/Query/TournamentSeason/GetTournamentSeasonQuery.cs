using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetTournamentSeasonQuery : IRequest<IEnumerable<TournamentSeason>>
    {
        public int Id { get; set; }
    }
    public class GetTournamentSeasonQueryHandler : IRequestHandler<GetTournamentSeasonQuery, IEnumerable<TournamentSeason>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTournamentSeasonQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<TournamentSeason>> Handle(GetTournamentSeasonQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.TournamentSeasonRepository.Get());
            return Task.FromResult(unitOfWork.TournamentSeasonRepository.Get(x => x.Id == request.Id));
        }
    }
}
