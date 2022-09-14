using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class DeleteTournamentRoundCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentRoundCommandHandler : IRequestHandler<DeleteTournamentRoundCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTournamentRoundCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IResultResponse> Handle(DeleteTournamentRoundCommand request, CancellationToken cancellationToken)
        {
            var tournamentRound = unitOfWork.TournamentRoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentRound != null)
            {
                unitOfWork.TournamentRoundRepository.Delete(tournamentRound);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
