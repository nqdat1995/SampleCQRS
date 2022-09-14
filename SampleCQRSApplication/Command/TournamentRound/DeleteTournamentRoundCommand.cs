using MediatR;
using SampleCQRSApplication.Data;

namespace SampleCQRSApplication.Command
{
    public class DeleteTournamentRoundCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentRoundCommandHandler : IRequestHandler<DeleteTournamentRoundCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTournamentRoundCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteTournamentRoundCommand request, CancellationToken cancellationToken)
        {
            var tournamentRound = unitOfWork.TournamentRoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentRound != null)
            {
                unitOfWork.TournamentRoundRepository.Delete(tournamentRound);
                await unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
