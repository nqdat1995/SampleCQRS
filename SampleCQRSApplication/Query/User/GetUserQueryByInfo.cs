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
    public class GetUserQueryByInfo : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class GetUserQueryByInfoHandler : IRequestHandler<GetUserQueryByInfo, User>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetUserQueryByInfoHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<User> Handle(GetUserQueryByInfo request, CancellationToken cancellationToken)
        {
            var user = unitOfWork.UserRepository.Get(x => (x.Username == request.Username || x.Email == request.Username) && x.Password == request.Password).FirstOrDefault();

            return Task.FromResult(user);
        }
    }
}
