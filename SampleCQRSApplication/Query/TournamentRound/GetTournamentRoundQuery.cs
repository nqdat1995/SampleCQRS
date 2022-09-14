using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetTournamentRoundQuery : IRequest<IEnumerable<TournamentRound>>
    {
        public int Id { get; set; }
    }
    public class GetTournamentRoundQueryHandler : IRequestHandler<GetTournamentRoundQuery, IEnumerable<TournamentRound>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTournamentRoundQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<TournamentRound>> Handle(GetTournamentRoundQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.TournamentRoundRepository.Get());
            return Task.FromResult(unitOfWork.TournamentRoundRepository.Get(x => x.Id == request.Id));
        }
    }
}
