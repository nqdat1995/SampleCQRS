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
    public class GetRoundByTournamentQuery : IRequest<IEnumerable<Round>>
    {
        public int TournamentId { get; set; }
    }
    public class GetRoundByTournamentQueryHandler : IRequestHandler<GetRoundByTournamentQuery, IEnumerable<Round>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetRoundByTournamentQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Round>> Handle(GetRoundByTournamentQuery request, CancellationToken cancellationToken)
        {
            //var context = unitOfWork.Context();

            //var result = (from s in context.Seasons
            //              join st in context.TournamentSeasons on s.Id equals st.SeasonId
            //              join t in context.Tournaments on st.TournamentId equals t.Id
            //              where s.Id == request.SeasonId
            //              select t);

            //return Task.FromResult(result.AsEnumerable());

            var rounds = unitOfWork.TournamentRoundRepository.Get(x => x.TournamentId == request.TournamentId, null, x => x.Round).Select(x => x.Round);
            return Task.FromResult(rounds);
        }
    }
}
