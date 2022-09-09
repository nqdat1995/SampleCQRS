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
    public class DeleteBetCommand : IRequest<bool>
    {
        public Bet Bet { get; set; }
    }
    public class DeleteBetCommandHandler : IRequestHandler<DeleteBetCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteBetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteBetCommand request, CancellationToken cancellationToken)
        {
            var bet = unitOfWork.BetRepository.Get(filter: x => x.Id == request.Bet.Id).FirstOrDefault();

            if (bet != null)
            {
                unitOfWork.BetRepository.Delete(bet);
                await unitOfWork.Save();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
