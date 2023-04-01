using Autofac;
using Autofac.Builder;
using FluentAssertions;
using Gaming1.Infrastructure.Repositories.DependencyInjection;
using Xunit;

namespace Gaming1.Infrastructure.Repositories.UnitTests
{
    public class DependencyInjectionTests
    {
        private IContainer _container;

        [Fact]
        public void ResolveIRepository_Return_InMemoryRepository()
        {
            // Arrange
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInMemoryRepository();
            _container = containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents);

            // Act
            var result = _container.Resolve<IRepository<FakeModel>>();

            // Assert
            result.Should().BeAssignableTo<IRepository<FakeModel>>();
            result.Should().BeOfType<InMemoryRepository<FakeModel>>();
        }
    }
}