using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Gaming1.Application.Service.Mappings;
using Gaming1.Application.Services.Contracts.Responses;
using Gaming1.Domain.Models;
using Gaming1.Tests.Common.Mappings;
using System;
using System.Linq;
using Xunit;

namespace Gaming1.Application.Service.UnitTests.Mappings
{
    public class ServiceMappingProfileTests : MappingProfileTestBase
    {
        public override Action<IMapperConfigurationExpression> GetConfigurationProfiles()
        {
            return cfg => cfg.AddProfile<ServiceMappingProfile>();
        }

        [Theory, AutoData]
        public void CanMap_From_Game_To_GetGameResponse(Game src)
        {
            // Arrange
            var players = src.Players
                .Select(srcPlayer => new PlayerResponse { PlayerId = srcPlayer.PlayerId })
                .ToList();

            var expectedResult =
                new GetGameResponse
                {
                    Players = players,
                    GameId = src.GameId
                };

            // Act
            var result = Mapper.Map<GetGameResponse>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public void CanMap_From_Game_To_StartResponse(Game src)
        {
            // Arrange
            var expectedResult =
                new StartResponse
                {
                    GameId = src.GameId
                };

            // Act
            var result = Mapper.Map<StartResponse>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public void CanMap_From_Game_To_AddPlayersResponse(Game src)
        {
            // Arrange
            var players =
                src.Players
                    .Select(playerResponse => new PlayerResponse { PlayerId = playerResponse.PlayerId })
                    .ToList();

            var expectedResult =
                new AddPlayersResponse
                {
                    GameId = src.GameId,
                    Players = players
                };

            // Act
            var result = Mapper.Map<AddPlayersResponse>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory, AutoData]
        public void CanMap_From_Player_To_PlayerResponse(Player src)
        {
            // Arrange
            var expectedResult =
                new PlayerResponse
                {
                    PlayerId = src.PlayerId
                };

            // Act
            var result = Mapper.Map<PlayerResponse>(src);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}