using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetSeasonQuery : IRequest<IEnumerable<Season>>
    {
    }
    public class GetSeasonQueryHandler : IRequestHandler<GetSeasonQuery, IEnumerable<Season>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetSeasonQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Season>> Handle(GetSeasonQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.SeasonRepository.Get());
        }
    }
}
