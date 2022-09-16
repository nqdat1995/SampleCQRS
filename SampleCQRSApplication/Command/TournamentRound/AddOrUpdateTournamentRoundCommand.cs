using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentRoundCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public TournamentRoundRequest TournamentRound { get; set; }
    }
    public class AddOrUpdateTournamentRoundCommandHandler : IRequestHandler<AddOrUpdateTournamentRoundCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTournamentRoundCommand request, CancellationToken cancellationToken)
        {
            Round round = await unitOfWork.RoundRepository.GetByID(request.TournamentRound.RoundId);
            Tournament tournament = await unitOfWork.TournamentRepository.GetByID(request.TournamentRound.TournamentId);

            if (round == null || tournament == null)
                return ResultResponse.BuildResponse(0);

            if (request.Id == 0)
            {
                var temp = mapper.Map(request.TournamentRound, new TournamentRound());
                unitOfWork.TournamentRoundRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var tournamentRound = unitOfWork.TournamentRoundRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentRound == null)
            {
                return ResultResponse.BuildResponse(0);
            }
            
            unitOfWork.TournamentRoundRepository.Update(mapper.Map(request.TournamentRound, tournamentRound));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(tournamentRound.Id);
        }
    }
}
