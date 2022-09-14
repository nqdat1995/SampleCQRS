using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public TournamentRequest Tournament { get; set; }
    }
    public class AddOrUpdateTournamentCommandHandler : IRequestHandler<AddOrUpdateTournamentCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTournamentCommand request, CancellationToken cancellationToken)
        {
            if(request.Id == 0)
            {
                var temp = mapper.Map(request.Tournament, new Tournament());
                unitOfWork.TournamentRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var tournament = unitOfWork.TournamentRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return ResultResponse.BuildResponse(0);
            }
            
            unitOfWork.TournamentRepository.Update(mapper.Map(request.Tournament, tournament));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(tournament.Id);
        }
    }
}
