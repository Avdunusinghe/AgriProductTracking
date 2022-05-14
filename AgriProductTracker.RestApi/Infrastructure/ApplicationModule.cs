using AgriProductTracker.Business;
using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.RestApi.Infrastructure.Services;
using Autofac;

namespace AgriProductTracker.RestApi.Infrastructure
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


            builder.RegisterType<AuthService>()
                .As<IAuthService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
                .As<IIdentityService>()
                .SingleInstance();

            builder.RegisterType<CurrentUserService>()
               .As<ICurrentUserService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
               .As<IProductService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<CoreDataService>()
              .As<ICoreDataService>()
              .InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
             .As<IUserService>()
             .InstancePerLifetimeScope();

            builder.RegisterType<DeliveryServiceService>()
            .As<IDeliveryServiceService>()
            .InstancePerLifetimeScope();
        }
    }
}
