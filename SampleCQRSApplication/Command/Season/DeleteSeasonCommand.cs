using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class DeleteSeasonCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IResultResponse> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = unitOfWork.SeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (season != null)
            {
                unitOfWork.SeasonRepository.Delete(season);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
