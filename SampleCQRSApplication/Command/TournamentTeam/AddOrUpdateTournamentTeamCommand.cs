using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentTeamCommand : IRequest<bool>
    {
        public TournamentTeam TournamentTeam { get; set; }
    }
    public class AddOrUpdateTournamentTeamCommandHandler : IRequestHandler<AddOrUpdateTournamentTeamCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTournamentTeamCommand request, CancellationToken cancellationToken)
        {
            var tournamentTeam = unitOfWork.TournamentTeamRepository.Get(filter: x => x.Id == request.TournamentTeam.Id).FirstOrDefault();

            if (tournamentTeam != null)
            {
                mapper.Map(request.TournamentTeam, tournamentTeam);
                unitOfWork.TournamentTeamRepository.Update(tournamentTeam);
                await unitOfWork.Save();
                return false;
            }

            unitOfWork.TournamentTeamRepository.Insert(request.TournamentTeam);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
