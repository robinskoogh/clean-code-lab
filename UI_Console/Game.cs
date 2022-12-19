using Application.Creators;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace UI_Console
{
    public class Game : IGame
    {
        private IGameEngine? _gameEngine;
        private readonly IScoreKeeper _scoreKeeper;
        private readonly IIOHelper _ioHelper;
        private readonly IConfiguration _config;
        private bool _gameHasBeenSelected = false;

        public Game(IScoreKeeper scoreKeeper, IIOHelper ioHelper, IConfiguration config)
        {
            _scoreKeeper = scoreKeeper;
            _ioHelper = ioHelper;
            _config = config;
        }

        public void SetGameEngine(IGameEngine gameEngine) => _gameEngine = gameEngine;

        public bool SelectGame()
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                _ioHelper.OutputMessage(ex.Message);
                return false;
            }
        }

        public void RunGame()
        {
            if (_gameEngine == null)
                _gameHasBeenSelected = SelectGame();

            if (_gameHasBeenSelected)
                _gameEngine?.StartGameEngine();
            else
                _ioHelper.OutputMessage("An error occurred while selecting the game. Please review the code and eventual error messages");
        }
    }
}
