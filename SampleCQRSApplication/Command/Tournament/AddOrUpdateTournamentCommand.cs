using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public TournamentRequest Tournament { get; set; }
    }
    public class AddOrUpdateTournamentCommandHandler : IRequestHandler<AddOrUpdateTournamentCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateTournamentCommand request, CancellationToken cancellationToken)
        {
            if(request.Id == 0)
            {
                unitOfWork.TournamentRepository.Insert(mapper.Map(request.Tournament, new Tournament()));
                await unitOfWork.Save();
                return true;
            }

            var tournament = unitOfWork.TournamentRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return false;
            }
            
            unitOfWork.TournamentRepository.Update(mapper.Map(request.Tournament, tournament));
            await unitOfWork.Save();
            return true;
        }
    }
}
