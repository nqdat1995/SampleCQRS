using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SampleCQRSApplication.Authentication
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IMediator mediator;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IMediator mediator)
        {
            _next = next;
            _appSettings = appSettings.Value;
            this.mediator = mediator;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                //context.Items["User"] = userService.GetById(userId.Value);
                context.Items["User"] = await mediator.Send(new GetUserQuery { Id = userId.Value });
            }

            await _next(context);
        }
    }
}
