using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentBySeasonCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public TournamentRequest Tournament { get; set; }
    }
    public class AddOrUpdateTournamentBySeasonCommandHandler : IRequestHandler<AddOrUpdateTournamentBySeasonCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AddOrUpdateTournamentBySeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTournamentBySeasonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var season = unitOfWork.SeasonRepository.GetByID(request.SeasonId);

                if (season == null)
                    return await Task.FromResult(ResultResponse.BuildResponse(0));

                var tournament = mapper.Map(request.Tournament, new Tournament());
                unitOfWork.TournamentRepository.Insert(tournament);
                await unitOfWork.Save();

                unitOfWork.TournamentSeasonRepository.Insert(new TournamentSeason { SeasonId = request.SeasonId, TournamentId = tournament.Id });

                return ResultResponse.BuildResponse(tournament.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return ResultResponse.BuildResponse(0);
            }
        }
    }
}
