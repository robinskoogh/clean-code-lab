using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Domain.Models;
using System.Text.RegularExpressions;

namespace Infrastructure
{
    public class BullsAndCowsScoreKeeper : IScoreKeeper
    {
        private const string Divider = "#&#";
        private string? _filename;

        private readonly IIOHelper _ioHelper;

        public BullsAndCowsScoreKeeper(IIOHelper ioHelper)
        {
            _ioHelper = ioHelper;
        }

        public void SetFilename(string filename)
        {
            Regex regex = new("^[a-zA-Z0-9._-]*$");

            if (!regex.IsMatch(filename))
                throw new InvalidFilenameException("Filename may only contain alphanumerical characters, including ._- without white-spaces.");

            _filename = filename;
        }

        public void WriteToFile(string username, int numberOfGuesses)
        {
            if (_filename == null)
                throw new FilenameNotSetException("You need to set the filename of the scorekeeper before using it");
            StreamWriter output = new(_filename, append: true);
            output.WriteLine(string.Concat(username, Divider, numberOfGuesses));
            output.Close();
        }

        public void DisplayTopList()
        {
            if (_filename == null)
                throw new FilenameNotSetException("You need to set the filename of the scorekeeper before using it");
            StreamReader resultsReader = new(_filename);
            List<ScoreCard> scoreCards = new();
            string? resultEntry;

            while ((resultEntry = resultsReader.ReadLine()) != null)
            {
                CompileScoreCardsFromResultEntries(resultEntry, out string name, out int numberOfGuesses);

                ScoreCard scoreCard = new(name, numberOfGuesses);

                if (!scoreCards.Contains(scoreCard))
                    scoreCards.Add(scoreCard);
                else
                    scoreCards.FirstOrDefault(pd => pd.Name == name)?.Update(numberOfGuesses);
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
