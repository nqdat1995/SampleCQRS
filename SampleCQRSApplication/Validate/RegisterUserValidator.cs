using FluentValidation;
using SampleCQRSApplication.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Validate
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().Matches(@"^[a-z0-9](\.?[a-z0-9]){5,}@sacombank.com");
        }
    }
}
