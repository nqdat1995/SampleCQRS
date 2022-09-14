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
    public class DeleteMatchCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteMatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IResultResponse> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var match = unitOfWork.MatchRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (match != null)
            {
                unitOfWork.MatchRepository.Delete(match);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(request.Id);
            }

            return ResultResponse.BuildResponse(0);
        }
    }
}
