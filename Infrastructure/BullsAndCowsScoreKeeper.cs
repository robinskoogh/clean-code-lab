using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using System.Text.RegularExpressions;

namespace Infrastructure
{
    public class BullsAndCowsScoreKeeper : IScoreKeeper
    {
        public string? Filename { get; private set; }

        private const string Divider = "#&#";
        private readonly IIOHelper _ioHelper;

        public BullsAndCowsScoreKeeper(IIOHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public void SetFilename(string filename)
        {
            if (filename == null)
                throw new FilenameNotSetException("The filename was was null and has not been set. Please review the call to _scoreKeeper.SetFilename() in Game.cs");

            Regex regex = new("^[a-zA-Z0-9._-]*$");

            if (!regex.IsMatch(filename))
                throw new InvalidFilenameException("Filename may only contain alphanumerical characters, including ._- without white-spaces.");

            Filename = filename;
        }

        public void WriteToFile(string username, int numberOfGuesses)
        {
            if (Filename == null)
                throw new FilenameNotSetException("You need to set the filename of the scorekeeper before using it");
            StreamWriter output = new(Filename, append: true);
            output.WriteLine(string.Concat(username, Divider, numberOfGuesses));
            output.Close();
        }

        public void DisplayTopList()
        {
            if (Filename == null)
                throw new FilenameNotSetException("You need to set the filename of the scorekeeper before using it");
            StreamReader resultsReader = new(Filename);
            List<ScoreCard> scoreCards = new();
            string? resultEntry;

            while ((resultEntry = resultsReader.ReadLine()) != null)
            {
                CompileScoreCardsFromResultEntries(resultEntry, out string name, out int numberOfGuesses);

                ScoreCard scoreCard = new(name, numberOfGuesses);

                var existingScoreCard = scoreCards.FirstOrDefault(sc => sc.Name == name);

                if (existingScoreCard == null)
                    scoreCards.Add(scoreCard);
                else
                    existingScoreCard.Update(numberOfGuesses);
            }
            resultsReader.Close();

            PrintHighScore(scoreCards, 5);
        }

        private void PrintHighScore(List<ScoreCard> topList, int sizeOfHighScoreList)
        {
            topList.Sort((playerOne, playerTwo) => playerOne.Average().CompareTo(playerTwo.Average()));

            _ioHelper.OutputMessage("\n----------- High Score -----------");
            _ioHelper.OutputMessage("Player\t\t| Games\t| Average");
            _ioHelper.OutputMessage("----------------------------------");
            foreach (ScoreCard player in topList.Take(sizeOfHighScoreList))
            {
                _ioHelper.OutputMessage($"{player.Name}\t\t| {player.GamesPlayed}\t| {player.Average():0.##}");
            }
            _ioHelper.OutputMessage("----------------------------------");
        }

        private static void CompileScoreCardsFromResultEntries(string line, out string name, out int guesses)
        {
            string[] nameAndScore = line.Split(new string[] { Divider }, StringSplitOptions.None);
            name = nameAndScore[0];
            guesses = int.Parse(nameAndScore[1]);
        }
    }
}
