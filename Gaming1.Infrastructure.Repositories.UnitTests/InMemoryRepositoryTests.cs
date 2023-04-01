using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Infrastructure.Repositories.UnitTests
{
    public class InMemoryRepositoryTests
    {
        private readonly InMemoryRepository<FakeModel> _sut;

        public InMemoryRepositoryTests()
        {
            _sut = new InMemoryRepository<FakeModel>();
        }

        [Theory, AutoData]
        public async Task Save_WhenModelNotExist_AddModel(FakeModel model)
        {
            // Arrange
            var expectedResult = model;

            // Act
            await _sut.Save(model);
            var result = await _sut.Get(x => x.Id.Equals(model.Id));

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public async Task Get_WhenModelExists_Return_Model(FakeModel model)
        {
            // Arrange
            await _sut.Save(model);
            var expectedResult = model;

            // Act
            var result = await _sut.Get(x => x.Id.Equals(model.Id));

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public async Task Get_WhenModelNotExist_Return_Null(
            FakeModel model,
            Guid anotherId)
        {
            // Arrange
            await _sut.Save(model);

            // Act
            var result = await _sut.Get(x => x.Id.Equals(anotherId));

            // Assert
            result.Should().BeNull();
        }

        [Theory, AutoData]
        public async Task Get_WhenDatabaseIsEmpty_Return_Null(
            Guid id)
        {
            // Arrange

            // Act
            var result = await _sut.Get(x => x.Id.Equals(id));

            // Assert
            result.Should().BeNull();
        }
    }
}