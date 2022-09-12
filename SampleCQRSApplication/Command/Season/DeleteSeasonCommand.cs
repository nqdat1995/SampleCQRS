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
    public class DeleteSeasonCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = unitOfWork.SeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (season != null)
            {
                unitOfWork.SeasonRepository.Delete(season);
                await unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
