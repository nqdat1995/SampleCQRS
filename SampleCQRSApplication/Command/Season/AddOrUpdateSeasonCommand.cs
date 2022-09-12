using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateSeasonCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public SeasonRequest Season { get; set; }
    }
    public class AddOrUpdateSeasonCommandHandler : IRequestHandler<AddOrUpdateSeasonCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateSeasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateSeasonCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                unitOfWork.SeasonRepository.Insert(mapper.Map(request.Season, new Season()));
                await unitOfWork.Save();
                return true;
            }

            var season = unitOfWork.SeasonRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (season == null)
                return false;

            unitOfWork.SeasonRepository.Update(mapper.Map(request.Season, season));
            await unitOfWork.Save();
            return true;
        }
    }
}
