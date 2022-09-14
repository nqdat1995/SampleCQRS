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
    public class GetTournamentBySeasonQuery : IRequest<ICollection<Tournament>>
    {
        public int SeasonId { get; set; }
    }
    public class GetTournamentBySeasonQueryHandler : IRequestHandler<GetTournamentBySeasonQuery, ICollection<Tournament>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTournamentBySeasonQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ICollection<Tournament>> Handle(GetTournamentBySeasonQuery request, CancellationToken cancellationToken)
        {
            var tournaments = unitOfWork.TournamentSeasonRepository.Get(x => x.SeasonId == request.SeasonId, null, x => x.Tournaments).FirstOrDefault();
            return Task.FromResult(tournaments?.Tournaments);
        }
    }
}
