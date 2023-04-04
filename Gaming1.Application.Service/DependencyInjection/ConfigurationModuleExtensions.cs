using Autofac;
using Gaming1.Application.Service.Resolvers;

namespace Gaming1.Application.Service.DependencyInjection
{
    public static class ConfigurationModuleExtensions
    {
        public static ContainerBuilder RegisterResolvers(
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

            return builder;
        }
    }
}