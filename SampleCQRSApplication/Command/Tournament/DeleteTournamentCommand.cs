using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Command
{
    public class DeleteTournamentCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTournamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IResultResponse> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = unitOfWork.TournamentRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament != null)
            {
                unitOfWork.TournamentRepository.Delete(tournament);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
