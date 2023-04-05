using Gaming1.Api.Contracts.Game;
using Gaming1.Domain.Models;
using Gaming1.EndToEndTests.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Gaming1.EndToEndTests.Features.Game
{

    public class GameSteps : GameBaseFeatureTests
    {
        [Given(@"A Game started")]
        public async Task GivenAGameStarted()
        {
            var game = await GetResultDataFromUrl<StartResult>(StartGameUrl);
            Game.GameId = game.GameId;
            Game.Players = new List<Player>();
        }

        [Given(@"(.*) players are added to the game")]
        public async Task GivenPlayersAreAddedToTheGame(int playerNumber)
        {
            var payload = new AddPlayersPayload { PlayersNumber = playerNumber };
            var url = string.Format(AddPlayersGameUrl, Game.GameId);

            var players = await GetResultDataFromUrl<AddPlayersResult>(url, payload);
            foreach (var player in players.Players)
            {
                Game.Players.Add(new Player { PlayerId = player.PlayerId });
            }
        }

        [When(@"Each player suggest a number until one of them hits the secret number")]
        public async Task WhenEachPlayerSuggestANumberUntilOneOfThemHitsTheSecretNumber()
        {
            var gameIsFinished = false;

            while (!gameIsFinished)
            {
                foreach (var gamePlayer in Game.Players)
                {
                    var payload = new SuggestNumberPayload { SuggestedNumber = new Random().Next(1, 100) };
                    var url = string.Format(PlayGameUrl, Game.GameId, gamePlayer.PlayerId);

                    var result = await GetResultDataFromUrl<SuggestNumberResult>(url, payload);

                    if (result.ResultMessage.Equals($"Yes! The secret number is {payload.SuggestedNumber}. You are the winner!", StringComparison.InvariantCultureIgnoreCase))
                    {
                        gameIsFinished = true;
                    }
                }
            }
        }

        [Then(@"The Game is finished")]
        public void ThenTheGameIsFinished()
        {
            // Nothing for the moment. Here we could check a async notification or database data.
        }
    }
}