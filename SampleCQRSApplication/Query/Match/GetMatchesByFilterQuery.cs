using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Query
{
    public class GetMatchesByFilterQuery : IRequest<IEnumerable<Match>>
    {
        public int RoundId { get; set; }
        public int TeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public int PageNumber { get; set; } = 1;
    }
    public class GetMatchesByFilterQueryHandler : IRequestHandler<GetMatchesByFilterQuery, IEnumerable<Match>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetMatchesByFilterQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Match>> Handle(GetMatchesByFilterQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber == 0 ? 1 : request.PageNumber;
            var matches = unitOfWork.MatchRepository.Get(x =>
                (x.RoundId == request.RoundId || request.RoundId == 0) &&
                (x.HomeTeamId == request.TeamId || x.VisitingTeamId == request.TeamId || request.TeamId == 0) &&
                (x.MatchDate == request.MatchDate || true)
            ).Skip((request.PageNumber - 1) * 10).Take(10);

            return Task.FromResult(matches);
        }
    }
}
