using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Authentication
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin", Role = Role.Admin },
            new User { Id = 2, Username = "test", Password = "test", Role = Role.User }
        };

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
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
