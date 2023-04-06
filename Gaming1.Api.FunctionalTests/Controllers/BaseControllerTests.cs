using Gaming1.Api.Contracts.Game.Payloads;
using Gaming1.Api.Contracts.Game.Results;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Gaming1.Api.FunctionalTests.Controllers
{
    [CollectionDefinition("Functional Tests", DisableParallelization = true)]
    public class BaseControllerTests : FunctionalTestWebApplicationFactory<TestStartup>
    {
        protected HttpClient ApiClient;

        public BaseControllerTests()
        {
            ApiClient = CreateClient();
        }

        protected async Task<Guid> StartGame()
        {
            var game = await GetResultDataFromUrl<StartResult>(TestConstants.StartGameUrl);
            return game.GameId;
        }

        protected async Task<Guid> AddPlayer(Guid gameId)
        {
            var payload = new AddPlayersPayload() { PlayersNumber = 1 };
            var url = string.Format(TestConstants.AddPlayersGameUrl, gameId);

            var game = await GetResultDataFromUrl<AddPlayersResult>(url, payload);
            return game.Players.First().PlayerId;
        }
    }
}