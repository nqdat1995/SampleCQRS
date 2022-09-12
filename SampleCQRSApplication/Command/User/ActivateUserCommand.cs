using AutoMapper;
using MediatR;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Request;

namespace SampleCQRSApplication.Command
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public RegisterUserRequest RegisterUser { get; set; }
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = unitOfWork.UserRepository.Get(filter: x => x.Email == request.RegisterUser.Email).FirstOrDefault();

            if (user != null)
            {
                return await Task.FromResult(false);
            }

            unitOfWork.UserRepository.Insert(mapper.Map(request.RegisterUser, new User()));
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
