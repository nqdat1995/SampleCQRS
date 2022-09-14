using AutoMapper;
using MediatR;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public RegisterUserRequest RegisterUser { get; set; }
    }
    public class AddOrUpdateUserCommandHandler : IRequestHandler<AddOrUpdateUserCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AddOrUpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = unitOfWork.UserRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (user != null)
            {
                mapper.Map(request.RegisterUser, user);
                unitOfWork.UserRepository.Update(user);
                await unitOfWork.Save();
                return true;
            }

            unitOfWork.UserRepository.Insert(mapper.Map(request.RegisterUser, new User()));
            await unitOfWork.Save();
            return await Task.FromResult(true);
        }
    }
}
