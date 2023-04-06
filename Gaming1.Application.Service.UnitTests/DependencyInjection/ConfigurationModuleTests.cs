using Autofac;
using Autofac.Builder;
using FluentAssertions;
using Gaming1.Application.Service.DependencyInjection;
using Gaming1.Application.Service.Resolvers;
using Gaming1.Application.Service.Services;
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

        [Fact]
        public void ResolveISecretNumberGenerator_Return_SecretNumberGenerator()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterHandlers();
            _container = containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents);

            // Act
            var result = _container.Resolve<ISecretNumberGenerator>();

            // Assert
            result.Should().BeAssignableTo<ISecretNumberGenerator>();
            result.Should().BeOfType<SecretNumberGenerator>();
        }

        [Fact]
        public void ResolveIPlayerGenerator_Return_PlayerGenerator()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterHandlers();
            _container = containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents);

            // Act
            var result = _container.Resolve<IPlayerGenerator>();

            // Assert
            result.Should().BeAssignableTo<IPlayerGenerator>();
            result.Should().BeOfType<PlayerGenerator>();
        }

        [Fact]
        public void ResolveIGameFactory_Return_GameFactory()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterHandlers();
            _container = containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents);

            // Act
            var result = _container.Resolve<IGameFactory>();

            // Assert
            result.Should().BeAssignableTo<IGameFactory>();
            result.Should().BeOfType<GameFactory>();
        }
    }
}
