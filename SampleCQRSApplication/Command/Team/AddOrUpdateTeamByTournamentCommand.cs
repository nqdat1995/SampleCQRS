using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTeamByTournamentCommand : IRequest<IResultResponse>
    {
        public int TournamentId { get; set; }
        public TeamRequest Team { get; set; }
    }
    public class AddOrUpdateTeamByTournamentCommandHandler : IRequestHandler<AddOrUpdateTeamByTournamentCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AddOrUpdateTeamByTournamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTeamByTournamentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tournament = unitOfWork.TournamentRepository.GetByID(request.TournamentId);

                if (tournament == null)
                    return await Task.FromResult(ResultResponse.BuildResponse(0));

                var team = mapper.Map(request.Team, new Team());
                unitOfWork.TeamRepository.Insert(team);
                await unitOfWork.Save();

                unitOfWork.TournamentTeamRepository.Insert(new TournamentTeam { TeamId = team.Id, TournamentId = request.TournamentId });

                return ResultResponse.BuildResponse(team.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return ResultResponse.BuildResponse(0);
            }
        }
    }
}
