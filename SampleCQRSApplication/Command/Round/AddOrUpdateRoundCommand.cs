using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateRoundCommand : IRequest<bool>
    {
        public Round Round { get; set; }
    }
    public class AddOrUpdateRoundCommandHandler : IRequestHandler<AddOrUpdateRoundCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateRoundCommand request, CancellationToken cancellationToken)
        {
            var round = unitOfWork.RoundRepository.Get(filter: x => x.Id == request.Round.Id).FirstOrDefault();

            if (round != null)
            {
                mapper.Map(request.Round, round);
                unitOfWork.RoundRepository.Update(round);
                await unitOfWork.Save();
                return false;
            }

            unitOfWork.RoundRepository.Insert(request.Round);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
