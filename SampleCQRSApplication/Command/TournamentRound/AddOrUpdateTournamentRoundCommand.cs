using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentRoundCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public TournamentRoundRequest TournamentRound { get; set; }
    }
    public class AddOrUpdateTournamentRoundCommandHandler : IRequestHandler<AddOrUpdateTournamentRoundCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTournamentRoundCommand request, CancellationToken cancellationToken)
        {
            if(request.Id == 0)
            {
                unitOfWork.TournamentRoundRepository.Insert(mapper.Map(request.TournamentRound, new TournamentRound()));
                await unitOfWork.Save();
                return true;
            }

            var tournament = unitOfWork.TournamentRoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return false;
            }
            
            unitOfWork.TournamentRoundRepository.Update(mapper.Map(request.TournamentRound, tournament));
            await unitOfWork.Save();
            return true;
        }
    }
}
