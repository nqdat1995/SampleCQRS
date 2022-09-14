using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentSeasonCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public TournamentSeasonRequest TournamentSeason { get; set; }
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
            if(request.Id == 0)
            {
                unitOfWork.TournamentSeasonRepository.Insert(mapper.Map(request.TournamentSeason, new TournamentSeason()));
                await unitOfWork.Save();
                return true;
            }

            var tournament = unitOfWork.TournamentSeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return false;
            }
            
            unitOfWork.TournamentSeasonRepository.Update(mapper.Map(request.TournamentSeason, tournament));
            await unitOfWork.Save();
            return true;
        }
    }
}
