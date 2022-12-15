using Application.Interfaces;
using Domain.Enums.RockPaperScissors;

namespace Application.GameEngines
{
    public class RockPaperScissorsGameEngine : IGameEngine
    {
        private readonly IIOHelper _ioHelper;
        private readonly IScoreKeeper _scoreKeeper;
        private readonly List<string> _choices;
        //private readonly string[] _choices = new string[] { "rock", "paper", "scissors" };

        public RockPaperScissorsGameEngine(IIOHelper ioHelper, IScoreKeeper scoreKeeper)
        {
            _ioHelper = ioHelper;
            _scoreKeeper = scoreKeeper;
            _choices = new();

            foreach (var item in Enum.GetValues(typeof(Choice)).Cast<Choice>().ToArray())
            {
                _choices.Add(item.ToString());
            }
        }

        public void StartGameEngine()
        {
            string computerChoice = RandomizeComputerChoice();

            string userChoice = GetUserChoice();
        }

        private string GetUserChoice()
        {
            _ioHelper.OutputMessage("Select Rock, Paper or Scissors");

            string userChoice = _ioHelper.PromptStringInput("\n> ", "").Trim().ToLower();

            while (!_choices.Contains(userChoice))
            {
                userChoice = _ioHelper.PromptStringInput("You may only choose between Rock, Paper and Scissors\n> ", "").Trim().ToLower();
            }

            return userChoice;
        }

        private string RandomizeComputerChoice()
        {
            Random random = new Random();
            var choice = _choices[random.Next(0, _choices.Count)];

            return choice;
        }
    }
}
