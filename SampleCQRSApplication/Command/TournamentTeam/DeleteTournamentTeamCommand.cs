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
    public class DeleteTournamentTeamCommand : IRequest<bool>
    {
        public TournamentTeam TournamentTeam { get; set; }
    }
    public class DeleteTournamentTeamCommandHandler : IRequestHandler<DeleteTournamentTeamCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTournamentTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTournamentTeamCommand request, CancellationToken cancellationToken)
        {
            var tournamentTeam = unitOfWork.TournamentTeamRepository.Get(filter: x => x.Id == request.TournamentTeam.Id).FirstOrDefault();

            if (tournamentTeam != null)
            {
                unitOfWork.TournamentTeamRepository.Delete(tournamentTeam);
                await unitOfWork.Save();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
