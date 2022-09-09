using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateBetCommand : IRequest<bool>
    {
        public Bet Bet { get; set; }
    }
    public class AddOrUpdateBetCommandHandler : IRequestHandler<AddOrUpdateBetCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateBetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateBetCommand request, CancellationToken cancellationToken)
        {
            var bet = unitOfWork.BetRepository.Get(filter: x => x.Id == request.Bet.Id).FirstOrDefault();

            if (bet != null)
            {
                mapper.Map(request.Bet, bet);
                unitOfWork.BetRepository.Update(bet);
                await unitOfWork.Save();
                return true;
            }

            unitOfWork.BetRepository.Insert(request.Bet);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
