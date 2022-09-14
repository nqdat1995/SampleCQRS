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
    public class GetTournamentBySeasonQuery : IRequest<IEnumerable<Tournament>>
    {
        public int SeasonId { get; set; }
    }
    public class GetTournamentBySeasonQueryHandler : IRequestHandler<GetTournamentBySeasonQuery, IEnumerable<Tournament>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTournamentBySeasonQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Tournament>> Handle(GetTournamentBySeasonQuery request, CancellationToken cancellationToken)
        {
            var context = unitOfWork.Context();

            var result = (from s in context.Seasons
                          join st in context.TournamentSeasons on s.Id equals st.SeasonId
                          join t in context.Tournaments on st.TournamentId equals t.Id
                          where s.Id == request.SeasonId
                          select t);
            return Task.FromResult(result.AsEnumerable());
        }
    }
}
