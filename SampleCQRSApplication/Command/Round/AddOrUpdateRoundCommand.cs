using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateRoundCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public RoundRequest Round { get; set; }
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
            if (request.Id == 0)
            {
                unitOfWork.RoundRepository.Insert(mapper.Map(request.Round, new Round()));
                await unitOfWork.Save();
                return true;
            }

            var Round = unitOfWork.RoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (Round == null)
            {
                return false;
            }

            unitOfWork.RoundRepository.Update(mapper.Map(request.Round, Round));
            await unitOfWork.Save();
            return true;
        }
    }
}
