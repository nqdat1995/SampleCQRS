using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class DeleteTeamCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IResultResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = unitOfWork.TeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (team != null)
            {
                unitOfWork.TeamRepository.Delete(team);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
