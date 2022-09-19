using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateRoundByTournamentCommand : IRequest<IResultResponse>
    {
        public int TournamentId { get; set; }
        public RoundRequest Round { get; set; }
    }
    public class AddOrUpdateRoundByTournamentCommandHandler : IRequestHandler<AddOrUpdateRoundByTournamentCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AddOrUpdateRoundByTournamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateRoundByTournamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tournament = unitOfWork.TournamentRepository.GetByID(request.TournamentId);

                if (tournament == null)
                    return await Task.FromResult(ResultResponse.BuildResponse(0));

                var round = mapper.Map(request.Round, new Round());
                unitOfWork.RoundRepository.Insert(round);
                await unitOfWork.Save();

                unitOfWork.TournamentRoundRepository.Insert(new TournamentRound { RoundId = round.Id, TournamentId = request.TournamentId });

                return ResultResponse.BuildResponse(round.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return ResultResponse.BuildResponse(0);
            }
        }
    }
}
