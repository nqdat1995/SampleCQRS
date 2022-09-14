using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateMatchCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public MatchRequest Match { get; set; }
    }
    public class AddOrUpdateMatchCommandHandler : IRequestHandler<AddOrUpdateMatchCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateMatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateMatchCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                unitOfWork.MatchRepository.Insert(mapper.Map(request.Match, new Match()));
                await unitOfWork.Save();
                return true;
            }

            var Match = unitOfWork.MatchRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (Match == null)
            {
                return false;
            }

            unitOfWork.MatchRepository.Update(mapper.Map(request.Match, Match));
            await unitOfWork.Save();
            return true;
        }
    }
}
