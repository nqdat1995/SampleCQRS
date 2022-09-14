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
    public class GetTournamentQuery : IRequest<IEnumerable<Tournament>>
    {
        public int Id { get; set; }
    }
    public class GetTournamentQueryHandler : IRequestHandler<GetTournamentQuery, IEnumerable<Tournament>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTournamentQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Tournament>> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.TournamentRepository.Get());
            return Task.FromResult(unitOfWork.TournamentRepository.Get(x => x.Id == request.Id));
        }
    }
}
