using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentSeasonCommand : IRequest<bool>
    {
        public TournamentSeason TournamentSeason { get; set; }
    }
    public class AddOrUpdateTournamentSeasonCommandHandler : IRequestHandler<AddOrUpdateTournamentSeasonCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTournamentSeasonCommand request, CancellationToken cancellationToken)
        {
            var tournamentSeason = unitOfWork.TournamentSeasonRepository.Get(filter: x => x.Id == request.TournamentSeason.Id).FirstOrDefault();

            if (tournamentSeason != null)
            {
                mapper.Map(request.TournamentSeason, tournamentSeason);
                unitOfWork.TournamentSeasonRepository.Update(tournamentSeason);
                await unitOfWork.Save();
                return false;
            }

            unitOfWork.TournamentSeasonRepository.Insert(request.TournamentSeason);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
