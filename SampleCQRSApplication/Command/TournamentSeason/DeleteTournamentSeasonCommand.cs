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
        public TournamentSeason TournamentSeason { get; set; }
    }
    public class DeleteTournamentSeasonCommandHandler : IRequestHandler<DeleteTournamentSeasonCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTournamentSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTournamentSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = unitOfWork.TournamentSeasonRepository.Get(filter: x => x.Id == request.TournamentSeason.Id).FirstOrDefault();

            if (season != null)
            {
                unitOfWork.TournamentSeasonRepository.Delete(season);
                await unitOfWork.Save();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
