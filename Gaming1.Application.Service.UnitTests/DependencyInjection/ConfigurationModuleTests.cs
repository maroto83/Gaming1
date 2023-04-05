using Autofac;
using Autofac.Builder;
using FluentAssertions;
using Gaming1.Application.Service.DependencyInjection;
using Gaming1.Application.Service.Resolvers;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.DependencyInjection
{
    public class ConfigurationModuleTests
    {
        private IContainer _container;

        [Fact]
        public void ResolveIGameResolver_Return_WinnerResolver()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterHandlers();
            _container = containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents);

            // Act
            var result = _container.Resolve<IGameResolver>();

            // Assert
            result.Should().BeAssignableTo<IGameResolver>();
            result.Should().BeOfType<WinnerResolver>();
        }
    }
}
