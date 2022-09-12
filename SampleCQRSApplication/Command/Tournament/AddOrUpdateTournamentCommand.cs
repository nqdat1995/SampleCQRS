using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentCommand : IRequest<bool>
    {
        public Tournament Tournament { get; set; }
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
            var tournament = unitOfWork.TournamentRepository.Get(filter: x => x.Id == request.Tournament.Id).FirstOrDefault();

            if (tournament != null)
            {
                mapper.Map(request.Tournament, tournament);
                unitOfWork.TournamentRepository.Update(tournament);
                await unitOfWork.Save();
                return false;
            }

            unitOfWork.TournamentRepository.Insert(request.Tournament);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
