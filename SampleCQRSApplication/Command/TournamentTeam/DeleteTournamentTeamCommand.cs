using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class DeleteTournamentTeamCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentTeamCommandHandler : IRequestHandler<DeleteTournamentTeamCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTournamentTeamCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IResultResponse> Handle(DeleteTournamentTeamCommand request, CancellationToken cancellationToken)
        {
            var tournamentTeam = unitOfWork.TournamentTeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentTeam != null)
            {
                unitOfWork.TournamentTeamRepository.Delete(tournamentTeam);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(tournamentTeam.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
