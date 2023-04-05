using Gaming1.Domain.Models;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Gaming1.EndToEndTests.Base
{
    [Binding]
    public class GameBaseFeatureTests : EndToEndTestWebApplicationFactory<TestStartup>
    {
        protected const string GetGameUrl = "game/{0}";
        protected const string StartGameUrl = "game/start";
        protected const string AddPlayersGameUrl = "game/{0}/players/add";
        protected const string PlayGameUrl = "game/{0}/players/{1}/play";

        protected HttpClient GameApiClient;
        protected Game Game { get; set; } = new();

        public GameBaseFeatureTests()
        {
            GameApiClient = CreateClient();
        }
    }
}