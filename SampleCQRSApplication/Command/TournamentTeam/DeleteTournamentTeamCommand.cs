using MediatR;
using SampleCQRSApplication.Data;

namespace SampleCQRSApplication.Command
{
    public class DeleteTournamentTeamCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentTeamCommandHandler : IRequestHandler<DeleteTournamentTeamCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTournamentTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteTournamentTeamCommand request, CancellationToken cancellationToken)
        {
            var tournamentTeam = unitOfWork.TournamentTeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentTeam != null)
            {
                unitOfWork.TournamentTeamRepository.Delete(tournamentTeam);
                await unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
