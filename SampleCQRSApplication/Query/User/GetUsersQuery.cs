using MediatR;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Query
{
    public class GetUsersQuery : IRequest<IList<User>>
    {
        public int Id { get; set; }
    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IList<User>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IList<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = unitOfWork.UserRepository.Get();

            return await Task.FromResult(users.ToList());
        }
    }
}
