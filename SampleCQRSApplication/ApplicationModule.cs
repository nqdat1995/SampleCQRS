using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleCQRSApplication.Command;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.Notify;

namespace SampleCQRSApplication
{
    public class ApplicationModule : Module
    {
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
                    dbContextOptionsBuilder.UseSqlServer("Server=172.20.17.48;Database=TESTDB;User Id=ssisuser;Password=qqq111!!!;");

                    return new AppDBContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
