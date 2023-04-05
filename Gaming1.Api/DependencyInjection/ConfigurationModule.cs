using Autofac;
using Gaming1.Application.Service.DependencyInjection;
using Gaming1.Infrastructure.Repositories.DependencyInjection;

namespace Gaming1.Api.DependencyInjection
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInMemoryRepository();
            builder.RegisterHandlers();
        }
    }
}