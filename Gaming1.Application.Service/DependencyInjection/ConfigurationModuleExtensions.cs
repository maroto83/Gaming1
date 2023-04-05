using Autofac;
using Gaming1.Application.Service.Resolvers;
using Gaming1.Application.Service.Services;

namespace Gaming1.Application.Service.DependencyInjection
{
    public static class ConfigurationModuleExtensions
    {
        public static ContainerBuilder RegisterHandlers(
            this ContainerBuilder builder)
        {
            builder.RegisterType<WinnerResolver>();
            builder.RegisterType<HigherResolver>();
            builder.RegisterType<LowerResolver>();

            builder.Register(c => new ChainOfResponsabilityBuilder<IGameResolver>()
                    .RegisterProcessor(c.Resolve<WinnerResolver>())
                    .RegisterProcessor(c.Resolve<HigherResolver>())
                    .RegisterProcessor(c.Resolve<LowerResolver>())
                    .Build())
                .As<IGameResolver>();

            builder.RegisterType<SecretNumberGenerator>().As<ISecretNumberGenerator>();

            return builder;
        }
    }
}