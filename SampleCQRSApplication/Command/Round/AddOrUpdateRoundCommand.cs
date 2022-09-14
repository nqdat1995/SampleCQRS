using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateRoundCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public RoundRequest Round { get; set; }
    }
    public class AddOrUpdateRoundCommandHandler : IRequestHandler<AddOrUpdateRoundCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateRoundCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var temp = mapper.Map(request.Round, new Round());
                unitOfWork.RoundRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var round = unitOfWork.RoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (round == null)
            {
                return ResultResponse.BuildResponse(0);
            }

            unitOfWork.RoundRepository.Update(mapper.Map(request.Round, round));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(round.Id);
        }
    }
}
