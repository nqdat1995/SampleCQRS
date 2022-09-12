using MediatR;
using Microsoft.Extensions.Configuration;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Command
{
    public class AddSendMailCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }
    public class AddSendMailCommandHandler : IRequestHandler<AddSendMailCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMailUtils mailUtils;
        private readonly ISHAUtils shaUtils;
        public AddSendMailCommandHandler(IUnitOfWork unitOfWork, IMailUtils mailUtils, ISHAUtils shaUtils)
        {
            this.unitOfWork = unitOfWork;
            this.mailUtils = mailUtils;
            this.shaUtils = shaUtils;
        }

        public async Task<bool> Handle(AddSendMailCommand request, CancellationToken cancellationToken)
        {
            var user = unitOfWork.UserRepository.Get(x => x.Email == request.Email).FirstOrDefault();

            var code = Guid.NewGuid().ToString();
            var sent = mailUtils.Send(request.Email, "Sample Subject", $"Your code is {code}");

            var sendMail = new SendMail
            {
                Email = request.Email,
                User = user,
                ValidateCode = shaUtils.GetHash(code),
                Sent = sent
            };

            unitOfWork.SendMailRepository.Insert(sendMail);
            await unitOfWork.Save();
            return true;
        }
    }
}
