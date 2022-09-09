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
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = unitOfWork.UserRepository.Get(x => x.Id == request.Id).FirstOrDefault();

            return Task.FromResult(user);
        }
    }
}
