using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTeamCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public TeamRequest Team { get; set; }
    }
    public class AddOrUpdateTeamCommandHandler : IRequestHandler<AddOrUpdateTeamCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = unitOfWork.TeamsRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (team != null)
            {
                mapper.Map(request.Team, team);
                unitOfWork.TeamsRepository.Update(team);
                await unitOfWork.Save();
                return true;
            }

            unitOfWork.TeamsRepository.Insert(mapper.Map(request.Team, new Team()));
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
