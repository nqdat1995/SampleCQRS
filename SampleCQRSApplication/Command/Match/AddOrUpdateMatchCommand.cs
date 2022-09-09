using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateMatchCommand : IRequest<bool>
    {
        public Match Match { get; set; }
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
            var match = unitOfWork.MatchRepository.Get(filter: x => x.Id == request.Match.Id).FirstOrDefault();

            if (match != null)
            {
                mapper.Map(request.Match, match);
                unitOfWork.MatchRepository.Update(match);
                await unitOfWork.Save();
                return true;
            }

            unitOfWork.MatchRepository.Insert(request.Match);
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
