using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentSeasonCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public TournamentSeasonRequest TournamentSeason { get; set; }
    }
    public class AddOrUpdateTournamentSeasonCommandHandler : IRequestHandler<AddOrUpdateTournamentSeasonCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTournamentSeasonCommand request, CancellationToken cancellationToken)
        {
            Season season = await unitOfWork.SeasonRepository.GetByID(request.TournamentSeason.SeasonId);
            Tournament tournament = await unitOfWork.TournamentRepository.GetByID(request.TournamentSeason.TournamentId);

            if (season == null || tournament == null)
                return ResultResponse.BuildResponse(0);

            if(request.Id == 0)
            {
                var temp = mapper.Map(request.TournamentSeason, new TournamentSeason());
                unitOfWork.TournamentSeasonRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var tournamentSeason = unitOfWork.TournamentSeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentSeason == null)
            {
                return ResultResponse.BuildResponse(0);
            }
            
            unitOfWork.TournamentSeasonRepository.Update(mapper.Map(request.TournamentSeason, tournamentSeason));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(0);
        }
    }
}
