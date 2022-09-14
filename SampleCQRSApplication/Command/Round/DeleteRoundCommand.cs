using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class DeleteRoundCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteRoundCommandHandler : IRequestHandler<DeleteRoundCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IResultResponse> Handle(DeleteRoundCommand request, CancellationToken cancellationToken)
        {
            var match = unitOfWork.RoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (match != null)
            {
                unitOfWork.RoundRepository.Delete(match);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
