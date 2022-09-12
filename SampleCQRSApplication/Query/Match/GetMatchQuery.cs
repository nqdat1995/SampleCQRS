using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetMatchQuery : IRequest<List<Match>>
    {
        public int RoundId { get; set; }
        public int HomeTeamId { get; set; }
        public int VisitingTeamId { get; set; }
        public DateTime MatchDate { get; set; }
    }
    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, List<Match>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetMatchQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Match>> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Match> matches = default(IEnumerable<Match>);
            if (request.RoundId != 0 || request.HomeTeamId != 0 || request.VisitingTeamId != 0)
                matches = unitOfWork.MatchRepository.Get(filter: (x) =>
                    (x.RoundId == request.RoundId || request.RoundId == 0) &&
                    (x.HomeTeamId == request.HomeTeamId || request.HomeTeamId == 0) &&
                    (x.VisitingTeamId == request.VisitingTeamId || request.VisitingTeamId == 0));
            else
                matches = unitOfWork.MatchRepository.Get();
            return await Task.FromResult(matches.ToList());
        }
    }
}
