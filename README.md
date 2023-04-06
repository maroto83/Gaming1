#  Gaming1

The design of my solution contains the following parts:
* [Api](#api)
* [Application](#application)
* [Domain](#domain)
* [Infrastructure](#infrastructure)
* [Tests](#tests)

## Api

For the Api I've followed a Rest design where the main resource is the game. That's the raison I've choosen these urls for the endpoints:

- GET 	BaseUrl/game/{gameId} : 						Get the game data.
- POST 	BaseUrl/game/{gameId}/start : 					Start a new game.
- POST 	BaseUrl/game/{gameId}/players/add :				Add Players to a started game.
- POST 	BaseUrl/game/{gameId}/players/{playerId}/play :	The selected player suggest a number to hits the secret number

Initially, all the endpoints were in the same GameController. Later, I've refactored to have each endpoint in its own controller. In additional, from the controller I'm sending a request using Mediator (MediatR) to the application layer and I'm waiting for a sync response to be able to see the data in the response of the api.

Finally, I've included Swagger to be able to run the different endpoints. The URL for Swagger is https://localhost:44364/swagger .

## Application

In this part, I have the handlers for each request that are sending the controllers. All of them has a base class with the common code. Apart of that, I've added some extra classes to separate the responsabilities of each class. For example, I've used a GameFactory to create the game data for the StartGameRequestHandler or the PlayerGenerator to create the players you want to add to a started game in the AddPlayersRequestHandlers. 

I'd like to highlight how the handler SuggestNumberRequestHandler is resolving if the suggested number is higher, lower or is exactly the secret number. I've applied a chain of responsability pattern to avoid to have all the conditionals inside of this handler. The code of this handler is very clean after that.

## Domain

This part contains the models Game and Player.

## Infrastructure

It has only the repository project. Finally, I've used an InMemoryRepository implementation to be practical but other implementation with a real database engine could be used without issues inheriting from the repository interface

## Tests

The code has been done following the TDD approach. So, all the classes with logic contain at least a unit test. Apart of that, I've added a functional test to test the dependencies injections and also if each endpoint is working fine separately. Finally, I've created a End to End Test simulating a real game with several players. I've used SpecFlow to write the scenario and 3 uses cases for 2, 3 and 4 players. 