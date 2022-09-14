using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.OpenApi.Models;
using SampleCQRSApplication;
using SampleCQRSApplication.Authentication;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Utils;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMailUtils, MailUtils>();
builder.Services.AddSingleton<ISHAUtils, SHAUtils>();
builder.Services.AddSingleton<ICommonUtils, CommonUtils>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddValidatorsFromAssemblyContaining<ApplicationModule>();

//builder.Services.AddMediatR(typeof(AddOrUpdateTeamCommandHandler).Assembly);
// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //containerBuilder.RegisterMediatR(typeof(Program).Assembly);
    containerBuilder.RegisterMediatR(typeof(ApplicationModule).Assembly);
    containerBuilder.RegisterModule(new ApplicationModule(builder.Configuration.GetConnectionString("Default")));
    //containerBuilder.RegisterModule(new ApplicationModule(builder.Configuration.GetConnectionString("MySQL")));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//{
//    var testUsers = new List<User>
//    {
//        new User { Username = "admin", Password = "admin", Role = Role.Admin, Status = UserStatus.Activated, Email = "admin@example.com" },
//        new User { Username = "test", Password = "test", Role = Role.User, Status = UserStatus.Activated, Email = "test@example.com" }
//    };

//    using var scope = app.Services.CreateScope();
//    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
//    await unitOfWork.UserRepository.InsertRange(testUsers);
//    await unitOfWork.Save();
//}

app.Run();
