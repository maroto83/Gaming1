using Autofac;

namespace Gaming1.Infrastructure.Repositories.DependencyInjection
{
    public static class ConfigurationModuleExtensions
    {
        public static ContainerBuilder RegisterInMemoryRepository(
            this ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(InMemoryRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            return builder;
        }
    }
}