using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTeamCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public TeamRequest Team { get; set; }
    }
    public class AddOrUpdateTeamCommandHandler : IRequestHandler<AddOrUpdateTeamCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTeamCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var temp = mapper.Map(request.Team, new Team());
                unitOfWork.TeamRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var team = unitOfWork.TeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (team == null)
                return ResultResponse.BuildResponse(0);

            unitOfWork.TeamRepository.Update(mapper.Map(request.Team, team));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(team.Id);
        }
    }
}
