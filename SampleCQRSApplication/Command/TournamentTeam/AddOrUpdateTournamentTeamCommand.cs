using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentTeamCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public TournamentTeamRequest TournamentTeam { get; set; }
    }
    public class AddOrUpdateTournamentTeamCommandHandler : IRequestHandler<AddOrUpdateTournamentTeamCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTournamentTeamCommand request, CancellationToken cancellationToken)
        {
            if(request.Id == 0)
            {
                unitOfWork.TournamentTeamRepository.Insert(mapper.Map(request.TournamentTeam, new TournamentTeam()));
                await unitOfWork.Save();
                return true;
            }

            var tournament = unitOfWork.TournamentTeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return false;
            }
            
            unitOfWork.TournamentTeamRepository.Update(mapper.Map(request.TournamentTeam, tournament));
            await unitOfWork.Save();
            return true;
        }
    }
}
