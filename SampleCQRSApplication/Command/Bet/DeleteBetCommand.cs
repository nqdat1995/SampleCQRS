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
    public class DeleteBetCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteBetCommandHandler : IRequestHandler<DeleteBetCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteBetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IResultResponse> Handle(DeleteBetCommand request, CancellationToken cancellationToken)
        {
            var bet = unitOfWork.MatchRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (bet != null)
            {
                unitOfWork.MatchRepository.Delete(bet);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
