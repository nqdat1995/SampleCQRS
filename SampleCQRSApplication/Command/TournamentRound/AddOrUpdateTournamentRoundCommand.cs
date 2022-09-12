using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentRoundCommand : IRequest<bool>
    {
        public TournamentRound TournamentRound { get; set; }
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
            var tournamentRound = unitOfWork.TournamentRoundRepository.Get(filter: x => x.Id == request.TournamentRound.Id).FirstOrDefault();

            if (tournamentRound != null)
            {
                mapper.Map(request.TournamentRound, tournamentRound);
                unitOfWork.TournamentRoundRepository.Update(tournamentRound);
                await unitOfWork.Save();
                return false;
            }

            unitOfWork.TournamentRoundRepository.Insert(request.TournamentRound);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
