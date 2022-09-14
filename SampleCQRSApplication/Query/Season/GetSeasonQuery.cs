using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Query
{
    public class GetSeasonQuery : IRequest<IEnumerable<Season>>
    {
        public int Id { get; set; }
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
            if (request.Id == 0)
                return Task.FromResult(unitOfWork.SeasonRepository.Get());

            return Task.FromResult(unitOfWork.SeasonRepository.Get(x => x.Id == request.Id));
        }
    }
}
