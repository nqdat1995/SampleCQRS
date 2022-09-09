using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleCQRSApplication.Data;

namespace SampleCQRSApplication
{
    public class ApplicationModule : Module
    {
        private readonly string connectionString;
        public ApplicationModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<AddOrUpdateTeamCommand>().AsImplementedInterfaces();
            //builder.RegisterType<PublishTeamNoify>().AsImplementedInterfaces();

            //builder.RegisterType<AddOrUpdateTeamCommandHandler>().AsImplementedInterfaces();
            //builder.RegisterType<PublishTeamMessageHandler>().AsImplementedInterfaces();
            //builder.RegisterType<PublishTeamTextHandler>().AsImplementedInterfaces();
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
                    dbContextOptionsBuilder.UseSqlServer(connectionString);

                    return new AppDBContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
