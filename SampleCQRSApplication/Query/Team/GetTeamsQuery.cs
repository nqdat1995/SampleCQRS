using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using System.Linq.Expressions;

namespace SampleCQRSApplication.Query
{
    public class GetTeamsQuery : IRequest<List<Team>>
    {
        public string Name { get; set; }
    }
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, List<Team>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTeamsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Team>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Team> teams = default(IEnumerable<Team>);
            if (!string.IsNullOrEmpty(request.Name))
                teams = unitOfWork.TeamsRepository.Get(filter: (x) => x.Name == request.Name);
            else
                teams = unitOfWork.TeamsRepository.Get();
            return await Task.FromResult(teams.ToList());
        }
    }
}
