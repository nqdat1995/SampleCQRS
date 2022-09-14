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
    public class DeleteTournamentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTournamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = unitOfWork.TournamentRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament != null)
            {
                unitOfWork.TournamentRepository.Delete(tournament);
                await unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
