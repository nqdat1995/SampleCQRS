using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class DeleteTeamCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = unitOfWork.TeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (team != null)
            {
                unitOfWork.TeamRepository.Delete(team);
                await unitOfWork.Save();
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
