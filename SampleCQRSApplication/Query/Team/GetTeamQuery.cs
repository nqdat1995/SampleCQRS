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
    public class GetTeamQuery : IRequest<IEnumerable<Team>>
    {
        public int Id { get; set; }
    }
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, IEnumerable<Team>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetTeamQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Team>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.TeamRepository.Get());
            return Task.FromResult(unitOfWork.TeamRepository.Get(x => x.Id == request.Id));
        }
    }
}
