using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateBetCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public Bet Bet { get; set; }
    }
    public class AddOrUpdateBetCommandHandler : IRequestHandler<AddOrUpdateBetCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateBetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateBetCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var temp = mapper.Map(request.Bet, new Bet());
                unitOfWork.BetRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var bet = unitOfWork.BetRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (bet == null)
            {
                return ResultResponse.BuildResponse(0);
            }

            unitOfWork.BetRepository.Update(mapper.Map(request.Bet, bet));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(bet.Id);
        }
    }
}
