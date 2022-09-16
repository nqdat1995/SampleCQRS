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
    public class GetTeamByTournamentQuery : IRequest<IEnumerable<Team>>
    {
        public int TournamentId { get; set; }
    }
    public class GetTeamByTournamentQueryHandler : IRequestHandler<GetTeamByTournamentQuery, IEnumerable<Team>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTeamByTournamentQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Team>> Handle(GetTeamByTournamentQuery request, CancellationToken cancellationToken)
        {
            //var context = unitOfWork.Context();

            //var result = (from s in context.Seasons
            //              join st in context.TournamentSeasons on s.Id equals st.SeasonId
            //              join t in context.Tournaments on st.TournamentId equals t.Id
            //              where s.Id == request.SeasonId
            //              select t);

            //return Task.FromResult(result.AsEnumerable());

            var teams = unitOfWork.TournamentTeamRepository.Get(x => x.TournamentId == request.TournamentId, null, x => x.Team).Select(x => x.Team);
            return Task.FromResult(teams);
        }
    }
}
