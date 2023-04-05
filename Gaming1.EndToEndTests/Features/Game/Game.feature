Feature: Game_Successful

Scenario Outline: Simulate a game with 2-4 players until one of them hits the secret number
	Given A Game started
	And <playerNumber> players are added to the game
	When Each player suggest a number until one of them hits the secret number
	Then The Game is finished

Examples:
	| playerNumber	|
	| 2				|
	| 3				|
	| 4				|