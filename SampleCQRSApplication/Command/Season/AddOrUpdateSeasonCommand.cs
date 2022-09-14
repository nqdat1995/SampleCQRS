using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateSeasonCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public SeasonRequest Season { get; set; }
    }
    public class AddOrUpdateSeasonCommandHandler : IRequestHandler<AddOrUpdateSeasonCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                var temp = mapper.Map(request.Season, new Season());
                unitOfWork.SeasonRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var season = unitOfWork.SeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (season == null)
                return ResultResponse.BuildResponse(0);

            unitOfWork.SeasonRepository.Update(mapper.Map(request.Season, season));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(season.Id);
        }
    }
}
