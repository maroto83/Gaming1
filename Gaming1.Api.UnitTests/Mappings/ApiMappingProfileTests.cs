using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Gaming1.Api.Contracts.Game;
using Gaming1.Api.Mapping;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Tests.Common.Mappings;
using System;
using System.Linq;
using Xunit;

namespace Gaming1.Api.UnitTests.Mappings
{
    public class ApiMappingProfileTests : MappingProfileTestBase
    {
        public override Action<IMapperConfigurationExpression> GetConfigurationProfiles()
        {
            return cfg => cfg.AddProfile<ApiMappingProfile>();
        }

        [Theory, AutoData]
        public void CanMap_From_GetGameResponse_To_GetGameResult(GetGameResponse src)
        {
            // Arrange
            var players = src.Players
                .Select(srcPlayer => new PlayerResult { PlayerId = srcPlayer.PlayerId })
                .ToList();

            var expectedResult =
                new GetGameResult
                {
                    Players = players,
                    GameId = src.GameId
                };

            // Act
            var result = Mapper.Map<GetGameResult>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public void CanMap_From_StartResponse_To_StartResult(StartResponse src)
        {
            // Arrange
            var expectedResult =
                new StartResult
                {
                    GameId = src.GameId
                };

            // Act
            var result = Mapper.Map<StartResult>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public void CanMap_From_AddPlayersResponse_To_AddPlayersResult(AddPlayersResponse src)
        {
            // Arrange
            var playerResults =
                src.Players
                    .Select(playerResponse => new PlayerResult { PlayerId = playerResponse.PlayerId })
                    .ToList();

            var expectedResult =
                new AddPlayersResult
                {
                    GameId = src.GameId,
                    Players = playerResults
                };

            // Act
            var result = Mapper.Map<AddPlayersResult>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public void CanMap_From_PlayerResponse_To_PlayerResult(PlayerResponse src)
        {
            // Arrange
            var expectedResult =
                new PlayerResult
                {
                    PlayerId = src.PlayerId
                };

            // Act
            var result = Mapper.Map<PlayerResult>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}