using System.Text.RegularExpressions;

namespace Laboration_Smells
{
    public class GameEngine
    {
        private readonly IOHelper _ioHelper;

        public GameEngine(IOHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public void RunGame(IOHelper _ioHelper)
        {
            const string VictorySequence = "BBBB,";
            const string GuestUsername = "guest";

            bool continuePlaying = true;
            bool changeUser = false;
            string name = string.Empty;

            while (continuePlaying)
            {
                if (string.IsNullOrEmpty(name) || changeUser)
                {
                    changeUser = false;
                    Console.Clear();
                    name = PromptUsername();
                }

                string correctAnswer = GenerateStringOfDigits();

                _ioHelper.OutputMessage(NewGameMessage(name, GuestUsername));

                int numberOfGuesses = PlayRound(VictorySequence, correctAnswer);

                if (name != GuestUsername)
                    ScoreKeeper.WriteToFile(name, numberOfGuesses);
                ScoreKeeper.DisplayTopList();

                _ioHelper.OutputMessage($"\n" +
                    $"Correct! It took you {numberOfGuesses} {(numberOfGuesses > 1 ? "guesses" : "guess")}\n\n" +
                    $"To play another round press Y\n" +
                    $"To switch to another user press C\n" +
                    $"To exit press any other key");

                string keepPlaying = Console.ReadKey(true).KeyChar.ToString().ToLower();

                if (keepPlaying != "y" && keepPlaying != "c")
                {
                    continuePlaying = false;
                }
                if (keepPlaying == "c")
                {
                    changeUser = true;
                }
            }
        }

        private string PromptUsername()
        {
            string name;
            do
            {
                name = _ioHelper.PromptStringInput("Enter your username: ", "guest");

                name = Normalize(name);

                if (name.Length > 9)
                {
                    _ioHelper.OutputMessage("Username can not be longer than 9 letters");
                    continue;
                }
            } while (name.Length > 9);
            return name;
        }

        private static string Normalize(string name)
        {
            return name.Trim().ToUpper();
        }

        private int PlayRound(string VictorySequence, string correctAnswer)
        {
            int numberOfGuesses = 0;
            string playerGuess;
            string resultOfGuess = string.Empty;

            _ioHelper.OutputMessage($"Enter guess ({correctAnswer})");

            while (resultOfGuess != VictorySequence)
            {
                numberOfGuesses++;
                playerGuess = _ioHelper.PromptStringInput("> ", "    ");

                if (string.IsNullOrWhiteSpace(playerGuess) || !Regex.IsMatch(playerGuess, "^[0-9]+$"))
                {
                    _ioHelper.OutputMessage("\nInvalid input. Guess must be 4 non-negative integers.\n");
                    continue;
                }

                resultOfGuess = EvaluatePlayerGuess(correctAnswer, playerGuess);
                _ioHelper.OutputMessage($"\n[{playerGuess}] {resultOfGuess}");
            }

            return numberOfGuesses;
        }

        private string NewGameMessage(string name, string guestUsername)
        {
            _ioHelper.ClearOutput();
            return name == guestUsername ? 
                $"Playing as a guest (your score will not be saved)\n" : 
                $"Player: {name}\nStarting a new game\n";
        }

        private static string GenerateStringOfDigits()
        {
            Random random = new();
            string goal = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                int randomNumber = random.Next(10);
                string randomDigit = randomNumber.ToString();
                while (goal.Contains(randomDigit))
                {
                    randomNumber = random.Next(10);
                    randomDigit = randomNumber.ToString();
                }
                goal += randomDigit;
            }
            return goal;
        }

        private static string EvaluatePlayerGuess(string goal, string guess)
        {
            int cows = 0, bulls = 0;
            for (int i = 0; i < goal.Length; i++)
            {
                for (int j = 0; j < guess.Length; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }

            return string.Concat("BBBB".AsSpan(0, bulls), ",", "CCCC".AsSpan(0, cows));
        }
    }
}
