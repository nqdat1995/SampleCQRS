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
    public class DeleteRoundCommand : IRequest<bool>
    {
        public Match Match { get; set; }
    }
    public class DeleteRoundCommandHandler : IRequestHandler<DeleteRoundCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteRoundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteRoundCommand request, CancellationToken cancellationToken)
        {
            var match = unitOfWork.MatchRepository.Get(filter: x => x.Id == request.Match.Id).FirstOrDefault();

            if (match != null)
            {
                unitOfWork.MatchRepository.Delete(match);
                await unitOfWork.Save();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
