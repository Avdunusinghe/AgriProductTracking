using AgriProductTracking.PaymentService.Infrastructure.Services;
using Autofac;

namespace AgriProductTracking.PaymentService.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>()
              .As<IHttpContextAccessor>()
              .SingleInstance();

            builder.RegisterType<IdentityService>()
               .As<IIdentityService>()
               .SingleInstance();

            builder.RegisterType<CurrentUserService>()
              .As<ICurrentUserService>()
              .InstancePerLifetimeScope();

        }
    }
}
