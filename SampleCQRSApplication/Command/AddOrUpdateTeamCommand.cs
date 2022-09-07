using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTeamCommand : IRequest<bool>
    {
        public Team Team { get; set; }
    }
    public class AddOrUpdateTeamCommandHandler : IRequestHandler<AddOrUpdateTeamCommand, bool>
    {
        private readonly TeamsInMemory teamsInMemory;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            teamsInMemory = new TeamsInMemory();
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTeamCommand request, CancellationToken cancellationToken)
        {
            //var team = teamsInMemory.Teams.FirstOrDefault(x => x.Name.Equals(request.Team.Name));
            var team = unitOfWork.TeamsRepository.Get(filter: x => x.Name == request.Team.Name).FirstOrDefault();

            if (team != null)
            {
                //var index = teamsInMemory.Teams.FindIndex(x => x.Name.Equals(request.Team.Name));
                mapper.Map(request.Team, team);
                unitOfWork.TeamsRepository.Update(team);
                await unitOfWork.Save();
                return true;
            }

            unitOfWork.TeamsRepository.Insert(request.Team);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
