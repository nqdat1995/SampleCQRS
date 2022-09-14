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
            if(request.Id == 0)
            {
                var temp = mapper.Map(request.TournamentSeason, new TournamentSeason());
                unitOfWork.TournamentSeasonRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var tournament = unitOfWork.TournamentSeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return ResultResponse.BuildResponse(0);
            }
            
            unitOfWork.TournamentSeasonRepository.Update(mapper.Map(request.TournamentSeason, tournament));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(0);
        }
    }
}
