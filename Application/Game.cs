using Application.Creators;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Application
{
    public class Game : IGame
    {
        private IGameEngine? _gameEngine;
        private readonly IScoreKeeper _scoreKeeper;
        private readonly IIOHelper _ioHelper;
        private readonly IConfiguration _config;

        public Game(IScoreKeeper scoreKeeper, IIOHelper ioHelper, IConfiguration config)
        {
            _scoreKeeper = scoreKeeper;
            _ioHelper = ioHelper;
            _config = config;
        }

        public void SetGameEngine(IGameEngine gameEngine) => _gameEngine = gameEngine;

        public void SelectGame()
        {
            _ioHelper.OutputMessage("Welcome to the Game Menu\n" +
                "Currently we only have one game available for playing:\n" +
                "1. Bulls and Cows\n");

            var userInput = _ioHelper.PromptStringInput("Please select a game by typing the corresponding number\n> ").Trim();

            while (userInput != "1")
            {
                userInput = _ioHelper.PromptStringInput("You may only select 1\n> ").Trim();
            }

            if (userInput == ((int)GameMode.BullsAndCows).ToString())
            {
                _scoreKeeper.SetFilename(_config.GetSection($"ScoreKeeperFileNames:BullsAndCows").Value!);
                _gameEngine = new BullsAndCowsGameEngineCreator(_ioHelper, _scoreKeeper).FetchGameEngine();
            }
        }

        public void RunGame()
        {
            if (_gameEngine == null)
                SelectGame();

            _gameEngine?.StartGameEngine();
        }
    }
}
