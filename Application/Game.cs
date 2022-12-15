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
                "You may choose between two games, which are:\n" +
                "1. Bulls and Cows\n" +
                "2. Rock, paper & scissors\n");

            var userInput = _ioHelper.PromptStringInput("Please select a game by typing the corresponding number\n> ", "").Trim();

            while (userInput != "1" && userInput != "2")
            {
                userInput = _ioHelper.PromptStringInput("You may only select 1 or 2\n> ", "").Trim();
            };

            if (userInput == ((int)GameMode.BullsAndCows).ToString())
            {
                _scoreKeeper.SetFilename(_config.GetSection($"ScoreKeeperFileNames:BullsAndCows").Value!);
                _gameEngine = new BullsAndCowsGameEngineCreator(_ioHelper, _scoreKeeper).FetchGameEngine();
            }

            else
            {
                _scoreKeeper.SetFilename(_config.GetSection($"ScoreKeeperFileNames:RockPaperScissors").Value!);
                _gameEngine = new RockPaperScissorsGameEngineCreator(_ioHelper, _scoreKeeper).FetchGameEngine();
            }
        }

        public void RunGame()
        {
            if (_gameEngine == null)
            {
                IGameEngine gameEngine = null;
                SetGameEngine(gameEngine);
            }

            _gameEngine.StartGameEngine();
        }
    }
}
