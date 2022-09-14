using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Query;
using SampleCQRSApplication.Message;

namespace SampleCQRS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserRequest> validator;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IValidator<RegisterUserRequest> validator, IMediator mediator, IMapper mapper)
        {
            _userService = userService;
            this.validator = validator;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUser)
        {
            ValidationResult result = await validator.ValidateAsync(registerUser);

            if (!result.IsValid)
                return BadRequest(result);

            var user = await mediator.Send(new RegisterUserCommand { RegisterUser = registerUser });

            if(!user)
            {
                return BadRequest("User existed");
            }

            await mediator.Send(mapper.Map(registerUser, new AddSendMailCommand()));

            return Ok();
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateUserRequest request)
        {
            var active = await mediator.Send(new ActivateUserCommand { ActivateUser = request });

            if (!active)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await mediator.Send(new GetUsersQuery());
            return Ok(users);
        }
    }
}
