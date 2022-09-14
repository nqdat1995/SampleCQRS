using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateMatchCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public MatchRequest Match { get; set; }
    }
    public class AddOrUpdateMatchCommandHandler : IRequestHandler<AddOrUpdateMatchCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateMatchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateMatchCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var temp = mapper.Map(request.Match, new Match());
                unitOfWork.MatchRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var match = unitOfWork.MatchRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (match == null)
            {
                return ResultResponse.BuildResponse(0);
            }

            unitOfWork.MatchRepository.Update(mapper.Map(request.Match, match));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(match.Id);
        }
    }
}
