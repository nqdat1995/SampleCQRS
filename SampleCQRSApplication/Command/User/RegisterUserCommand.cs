using AutoMapper;
using MediatR;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Message;
using SampleCQRSApplication.Utils;

namespace SampleCQRSApplication.Command
{
    public class ActivateUserCommand : IRequest<bool>
    {
        public ActivateUserRequest ActivateUser { get; set; }
    }
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISHAUtils shaUtils;

        public ActivateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISHAUtils shaUtils)
        {
            this.unitOfWork = unitOfWork;
            this.shaUtils = shaUtils;
        }

        public async Task<bool> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = unitOfWork.UserRepository.Get(filter: x => x.Email == request.ActivateUser.Email, null, x => x.SendMails).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var sendMails = user.SendMails.Where(x => !x.Used).FirstOrDefault();

            if (!shaUtils.VerifyHash(request.ActivateUser.ActivateCode, sendMails.ValidateCode))
            {
                return false;
            }

            user.Status = UserStatus.Activated;
            sendMails.Used = true;
            await unitOfWork.Save();
            return true;
        }
    }
}
