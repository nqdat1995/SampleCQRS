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
    public class GetMatchQuery : IRequest<IEnumerable<Match>>
    {
        public int Id { get; set; }
    }
    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, IEnumerable<Match>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetMatchQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Match>> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.MatchRepository.Get());
            return Task.FromResult(unitOfWork.MatchRepository.Get(x => x.Id == request.Id));
        }
    }
}
