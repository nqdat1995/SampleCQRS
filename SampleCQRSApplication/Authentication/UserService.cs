using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Message;
using SampleCQRSApplication.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleCQRSApplication.Authentication
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMediator mediator;

        public UserService(IOptions<AppSettings> appSettings, IJwtUtils jwtUtils, IMediator mediator)
        {
            _appSettings = appSettings.Value;
            _jwtUtils = jwtUtils;
            this.mediator = mediator;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            var user = await mediator.Send(new GetUserQueryByInfo { Username = model.Username, Password = model.Password });

            // return null if user not found
            if (user == null || user.Status == UserStatus.Activated) return null;

            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
    }
}
