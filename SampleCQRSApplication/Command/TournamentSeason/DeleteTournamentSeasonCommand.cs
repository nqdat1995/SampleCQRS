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
    public class DeleteTournamentSeasonCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentSeasonCommandHandler : IRequestHandler<DeleteTournamentSeasonCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTournamentSeasonCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteTournamentSeasonCommand request, CancellationToken cancellationToken)
        {
            var tournamentSeason = unitOfWork.TournamentSeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournamentSeason != null)
            {
                unitOfWork.TournamentSeasonRepository.Delete(tournamentSeason);
                await unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
