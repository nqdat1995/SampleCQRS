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
    public class GetRoundQuery : IRequest<IEnumerable<Round>>
    {
        public int Id { get; set; }
    }
    public class GetRoundQueryHandler : IRequestHandler<GetRoundQuery, IEnumerable<Round>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetRoundQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Round>> Handle(GetRoundQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.RoundRepository.Get());
            return Task.FromResult(unitOfWork.RoundRepository.Get(x => x.Id == request.Id));
        }
    }
}
