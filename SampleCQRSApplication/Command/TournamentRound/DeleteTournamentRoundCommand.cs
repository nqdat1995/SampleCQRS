using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Command
{
    public class DeleteTournamentRoundCommand : IRequest<bool>
    {
        public TournamentRound TournamentRound { get; set; }
    }
    public class DeleteTournamentRoundCommandHandler : IRequestHandler<DeleteTournamentRoundCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTournamentRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTournamentRoundCommand request, CancellationToken cancellationToken)
        {
            var tournamentRound = unitOfWork.TournamentRoundRepository.Get(filter: x => x.Id == request.TournamentRound.Id).FirstOrDefault();

            if (tournamentRound != null)
            {
                unitOfWork.TournamentRoundRepository.Delete(tournamentRound);
                await unitOfWork.Save();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
